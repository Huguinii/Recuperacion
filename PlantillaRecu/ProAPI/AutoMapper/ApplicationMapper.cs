using RestAPI.Models.DTOs.UserDto;
using AutoMapper;
using RestAPI.Models.DTOs;
using RestAPI.Models.Entity;
using RestAPI.Models.DTOs.DiaNoLectivoDTO;
using RestAPI.Models.DTOs.FranjaHorariaDTO;
using RestAPI.Models.DTOs.ReservaDTO;

namespace RestAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            // Ya existentes
            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<ReservaDTO, ReservaEntity>().ReverseMap();
            CreateMap<CreateReservaDTO, ReservaEntity>().ReverseMap();

            CreateMap<DiaDTO, DiaEntity>().ReverseMap();
            CreateMap<CreateDiaDTO, DiaEntity>().ReverseMap();

            CreateMap<FranjaHorariaDTO, FranjaHorariaEntity>().ReverseMap();
            CreateMap<CreateFranjaHorariaDTO, FranjaHorariaEntity>().ReverseMap();

            CreateMap<UsuarioDTO, UsuarioEntity>().ReverseMap();
            CreateMap<CreateUsuarioDTO, UsuarioEntity>().ReverseMap();

            
            CreateMap<EntidadN1DTO, EntidadN1Entity>().ReverseMap();
            CreateMap<CreateEntidadN1DTO, EntidadN1Entity>().ReverseMap();

            
            CreateMap<EntidadN2DTO, EntidadN2Entity>().ReverseMap();
            CreateMap<CreateEntidadN2DTO, EntidadN2Entity>().ReverseMap();

            CreateMap<EntidadN1N2, EntidadN1N2DTO>().ReverseMap();

        }
    }

}
