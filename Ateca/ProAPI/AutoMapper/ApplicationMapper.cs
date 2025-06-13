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
            
            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<ReservaDTO, ReservaEntity>().ReverseMap();
            CreateMap<CreateReservaDTO, ReservaEntity>().ReverseMap();

            CreateMap<DiaDTO, DiaEntity>().ReverseMap();
            CreateMap<CreateDiaDTO, DiaEntity>().ReverseMap();

            CreateMap<FranjaHorariaDTO, FranjaHorariaEntity>().ReverseMap();
            CreateMap<CreateFranjaHorariaDTO, FranjaHorariaEntity>().ReverseMap();

            CreateMap<UsuarioDTO, UsuarioEntity>().ReverseMap();
            CreateMap<CreateUsuarioDTO, UsuarioEntity>().ReverseMap();
        }
    }
}
