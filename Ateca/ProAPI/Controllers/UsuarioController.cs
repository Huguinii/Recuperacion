
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs;
using RestAPI.Models.DTOs.UserDto;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System.Net;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioEntity, UsuarioDTO, CreateUsuarioDTO>
    {
        public UsuarioController(IUsuarioRepository usuarioController,
            IMapper mapper, ILogger<UsuarioController> logger)
            : base(usuarioController, mapper, logger)
        {

        }
    }
}
