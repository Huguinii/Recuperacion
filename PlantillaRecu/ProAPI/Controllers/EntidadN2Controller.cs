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
    public class EntidadN2Controller : BaseController<EntidadN2Entity, EntidadN2DTO, CreateEntidadN2DTO>
    {
        public EntidadN2Controller(IEntidadN2Repository entidadN2Controller,
            IMapper mapper, ILogger<UsuarioController> logger)
            : base(entidadN2Controller, mapper, logger)
        {

        }
    }
}
