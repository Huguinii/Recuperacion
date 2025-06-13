using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.DTOs;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using RestAPI.Controllers;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.FranjaHorariaDTO;
using RestAPI.Repository;

namespace RestAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class FranjaHorariaController : BaseController<FranjaHorariaEntity, FranjaHorariaDTO, CreateFranjaHorariaDTO>
    {
        private readonly IFranjaHorariaRepository _franjaRepository;

        public FranjaHorariaController(
            IFranjaHorariaRepository repository,
            IMapper mapper,
            ILogger<FranjaHorariaController> logger
        ) : base(repository, mapper, logger)
        {
            _franjaRepository = repository;
        }

        // GET: api/FranjaHoraria/disponibles
        [HttpGet("disponibles")]
        public async Task<IActionResult> GetDisponibles()
        {
            try
            {
                var disponibles = await _franjaRepository.GetDisponiblesAsync();
                var result = _mapper.Map<List<FranjaHorariaDTO>>(disponibles);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener franjas disponibles.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // POST personalizado con validación de solapamiento
        [Authorize]
        [HttpPost("crear")]
        public async Task<IActionResult> CrearFranja([FromBody] CreateFranjaHorariaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.HoraInicio >= dto.HoraFin)
                return BadRequest("HoraInicio debe ser menor que HoraFin.");

            if (await SeSolapa(dto.HoraInicio, dto.HoraFin))
                return BadRequest("La franja se solapa con otra o no respeta los 5 minutos de separación.");

            var entity = _mapper.Map<FranjaHorariaEntity>(dto);
            await _repository.CreateAsync(entity);
            var result = _mapper.Map<FranjaHorariaDTO>(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, result);
        }

        // PUT personalizado con validación de solapamiento
        [Authorize]
        [HttpPut("{id:int}/editar")]
        public async Task<IActionResult> EditarFranja(int id, [FromBody] CreateFranjaHorariaDTO dto)
        {
            var existing = await _repository.GetAsync(id);
            if (existing == null) return NotFound();

            if (dto.HoraInicio >= dto.HoraFin)
                return BadRequest("HoraInicio debe ser menor que HoraFin.");

            if (await SeSolapa(dto.HoraInicio, dto.HoraFin, id))
                return BadRequest("La franja se solapa con otra o no respeta los 5 minutos de separación.");

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            var result = _mapper.Map<FranjaHorariaDTO>(existing);
            return Ok(result);
        }

        private async Task<bool> SeSolapa(TimeOnly inicio, TimeOnly fin, int? idExcluir = null)
        {
            var todas = await _repository.GetAllAsync();
            return todas.Any(f =>
                (idExcluir == null || f.Id != idExcluir) &&
                !(inicio >= f.HoraFin.AddMinutes(5) || fin <= f.HoraInicio.AddMinutes(-5))
            );
        }
    }
}
