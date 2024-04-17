using ApiCatalogoClientes.Common;
using ApiCatalogoClientes.Data.Dto;
using ApiCatalogoClientes.Data.Request;
using ApiCatalogoClientes.Data.Response;
using ApiCatalogoClientes.Domain.Entities;
using ApiCatalogoClientes.Interfaces;
using ApiCatalogoClientes.Utilities;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ApiCatalogoClientes.BussinessLogic
{
    public class BLClient
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public BLClient(ref IRepositoryWrapper repository, ref IMapper mapper, ref IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<GeneralResponse> GetAllActives()
        {
            var clients = await _repository.Clients.GetAllActives();

            if (clients is null)
            {
                return ResponseUtil.NotFound();
            }

            var result = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return ResponseUtil.Ok(default, result);
        }

        public async Task<GeneralResponse> GetById(int id)
        {
            var clientFounded = await _repository.Clients.FindByIdAsync(id);

            if (clientFounded is null)
            {
                return ResponseUtil.NotFound();
            }

            var result = _mapper.Map<ClientDto>(clientFounded);

            return ResponseUtil.Ok(default, result);
        }

        public async Task<GeneralResponse> Create(CreateUpdateClientRequest request)
        {
            bool clientExists = await _repository.Clients.ClientExistsByDocumentNumber(request.DocumentNumber.Trim());

            if (clientExists)
            {
                return ResponseUtil.Error("El número de documento ya existe");
            }

            Client newClient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = DateTime.Parse(request.BirthDate),
                IdTypeDocument = request.IdTypeDocument,
                DocumentNumber = request.DocumentNumber,
                FileNameCV = request.CvFileName,
                FileNamePhoto = request.PhotoFileName,
            };

            // UPLOAD FILES
            string dateFormated = DateTime.Now.ToString("yyyy MM dd HH mm ss").Replace(" ", "");
            Helper helper = new(_appSettings);

            // CV
            string[] partsCv = request.CvFileName.Split('.');
            string fileNameServerCv = string.Concat(partsCv[0], dateFormated, ".", partsCv[1]);

            await helper.UploadFileToS3(fileNameServerCv, request.CvBase64);
            newClient.FileNameCVServer = fileNameServerCv;

            // PHOTO
            string[] partsPhoto = request.PhotoFileName.Split('.');
            string fileNameServerPhoto = string.Concat(partsPhoto[0], dateFormated, ".", partsPhoto[1]);

            await helper.UploadFileToS3(fileNameServerPhoto, request.PhotoBase64);
            newClient.FileNamePhotoServer = fileNameServerPhoto;

            await _repository.Clients.Create(newClient);
            await _repository.SaveAsync();

            var clientCreated = await _repository.Clients.GetByDocumentNumber(request.DocumentNumber.Trim());

            if (clientCreated is null)
            {
                return ResponseUtil.Error("No pudo crearse el cliente");
            }
            else
            {
                var result = _mapper.Map<ClientDto>(clientCreated);

                return ResponseUtil.Create(default, result);
            }
        }

        public async Task<GeneralResponse> Update(CreateUpdateClientRequest request)
        {
            bool clientExists = await _repository.Clients.ClientExistsByDocumentNumber(request.DocumentNumber);

            if (!clientExists)
            {
                return ResponseUtil.NotFound("El cliente no está registrado");
            }

            var clientFounded = await _repository.Clients.GetByDocumentNumber(request.DocumentNumber.Trim());            

            clientFounded.FirstName = request.FirstName;
            clientFounded.LastName = request.LastName;
            clientFounded.BirthDate = DateTime.Parse(request.BirthDate);            
            clientFounded.UpdatedAt = DateTime.Now;

            // UPLOAD FILES
            string dateFormated = DateTime.Now.ToString("yyyy MM dd HH mm ss").Replace(" ", "");
            Helper helper = new(_appSettings);

            if (!string.IsNullOrEmpty(request.CvBase64))
            {
                string[] parts = request.CvFileName.Split('.');                
                string fileNameServer = string.Concat(parts[0], dateFormated, ".", parts[1]);

                await helper.UploadFileToS3(fileNameServer, request.CvBase64);
                clientFounded.FileNameCV = request.CvFileName;
                clientFounded.FileNameCVServer = fileNameServer;
            }

            if (!string.IsNullOrEmpty(request.PhotoBase64))
            {
                string[] parts = request.PhotoFileName.Split('.');
                string fileNameServer = string.Concat(parts[0], dateFormated, ".", parts[1]);

                await helper.UploadFileToS3(fileNameServer, request.PhotoBase64);
                clientFounded.FileNamePhoto = request.PhotoFileName;
                clientFounded.FileNamePhotoServer = fileNameServer;
            }
                        
            await _repository.SaveAsync();

            var clientUpdated = await _repository.Clients.GetByDocumentNumber(request.DocumentNumber.Trim());

            var result = _mapper.Map<ClientDto>(clientUpdated);

            return ResponseUtil.Ok(default, result);
        }

        public async Task<GeneralResponse> SoftDeleteById(DeleteClientRequest request)
        {
            var clientFounded = await _repository.Clients.FindByIdAsync(request.Id);

            if (clientFounded is null || !clientFounded.IsActive)
            {
                return ResponseUtil.NotFound(default);
            }

            await _repository.Clients.SoftDeleteById(request.Id);

            return ResponseUtil.Ok("Cliente eliminado con éxito", null);
        }
    }
}
