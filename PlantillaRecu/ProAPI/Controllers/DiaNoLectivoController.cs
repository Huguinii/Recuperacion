using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.Controllers;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.DiaNoLectivoDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DiaNoLectivoController : BaseController<DiaEntity, DiaDTO, CreateDiaDTO>
    {
        private readonly IDiaRepository _diaRepository;

        public DiaNoLectivoController(
            IDiaRepository diaRepository,
            IMapper mapper,
            ILogger<DiaNoLectivoController> logger)
            : base(diaRepository, mapper, logger)
        {
            _diaRepository = diaRepository;
        }

        // POST personalizado para evitar duplicados
        [Authorize]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearDiaNoLectivo([FromBody] CreateDiaDTO createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _diaRepository.EsDiaNoLectivo(createDto.Fecha))
                return BadRequest("Ya existe un día no lectivo para esa fecha.");

            var entity = _mapper.Map<DiaEntity>(createDto);
            await _diaRepository.CreateAsync(entity);

            var dto = _mapper.Map<DiaDTO>(entity);
            return CreatedAtAction(nameof(GetAll), null, dto);
        }
    }
}
