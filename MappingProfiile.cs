using ApiCatalogoClientes.Data.Dto;
using ApiCatalogoClientes.Domain.Entities;
using AutoMapper;

namespace ApiCatalogoClientes
{
    public class MappingProfiile : Profile
    {
        public MappingProfiile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.FileNameCv, opt => opt.MapFrom(src => src.FileNameCV))
                .ForMember(dest => dest.PathCv, opt => opt.MapFrom(src => string.Concat("https://catalogo-clientes.s3.us-east-2.amazonaws.com/", src.FileNameCVServer)))
                .ForMember(dest => dest.FileNamePhoto, opt => opt.MapFrom(src => src.FileNamePhoto))
                .ForMember(dest => dest.PathPhoto, opt => opt.MapFrom(src => string.Concat("https://catalogo-clientes.s3.us-east-2.amazonaws.com/", src.FileNamePhotoServer)))
                .ForMember(dest => dest.TypeDocument, opt => opt.MapFrom(src => src.TypeDocument));

            CreateMap<TypeDocument, TypeDocumentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
