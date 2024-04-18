using ApiCatalogoClientes.BussinessLogic;
using ApiCatalogoClientes.Common;
using ApiCatalogoClientes.Data.Request;
using ApiCatalogoClientes.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiCatalogoClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly BLClient bLClient;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public ClientController(IRepositoryWrapper repository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings;
            bLClient = new BLClient(ref _repository, ref _mapper, ref _appSettings);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllActives()
        {
            var result = await bLClient.GetAllActives();

            if (result.Status == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await bLClient.GetById(id);

            if (result.Status == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CreateUpdateClientRequest request)
        {
            var result = await bLClient.Create(request);

            if (result.Status == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(CreateUpdateClientRequest request)
        {
            var result = await bLClient.Update(request);

            if (result.Status == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SoftDelete(DeleteClientRequest request)
        {
            var result = await bLClient.SoftDeleteById(request);

            if (result.Status == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
