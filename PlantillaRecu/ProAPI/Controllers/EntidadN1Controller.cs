using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.UserDto;
using RestAPI.Models.DTOs;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using RestAPI.Models.DTOs.FranjaHorariaDTO;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadN1Controller : BaseController<EntidadN1Entity, EntidadN1DTO, CreateEntidadN1DTO>
    {
        public EntidadN1Controller(IEntidadN1Repository entidadN1Controller,
            IMapper mapper, ILogger<UsuarioController> logger)
            : base(entidadN1Controller, mapper, logger)
        {

        }
    }
}
