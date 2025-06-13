using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.ReservaDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository;
using RestAPI.Repository.IRepository;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ReservaController : BaseController<ReservaEntity, ReservaDTO, CreateReservaDTO>
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaController(IReservaRepository reservaRepository, IMapper mapper, ILogger<ReservaController> logger)
            : base(reservaRepository, mapper, logger)
        {
            _reservaRepository = reservaRepository;
        }

        // GET: /api/Reserva/pendientes
        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetPendientes()
        {
            var reservas = await _reservaRepository.GetReservasPendientesAsync();

            var dtoList = reservas.Select(r => _mapper.Map<ReservaDTO>(r)).ToList();
            return Ok(dtoList);
        }

        // PATCH: /api/Reserva/{id}/aprobar
        [HttpPatch("{id}/aprobar")]
        public async Task<IActionResult> Aprobar(int id)
        {
            var reserva = await _reservaRepository.GetAsync(id);
            if (reserva == null)
                return NotFound();

            if (reserva.Estado != "Pendiente")
                return BadRequest("La reserva ya fue procesada.");

            reserva.Estado = "Aprobada";
            var updated = await _reservaRepository.UpdateAsync(reserva);

            return updated ? NoContent() : StatusCode(500, "Error al aprobar la reserva.");
        }

        // PATCH: /api/Reserva/{id}/rechazar
        [HttpPatch("{id}/rechazar")]
        public async Task<IActionResult> Rechazar(int id)
        {
            var reserva = await _reservaRepository.GetAsync(id);
            if (reserva == null)
                return NotFound();

            if (reserva.Estado != "Pendiente")
                return BadRequest("La reserva ya fue procesada.");

            reserva.Estado = "Rechazada";
            var updated = await _reservaRepository.UpdateAsync(reserva);

            return updated ? NoContent() : StatusCode(500, "Error al rechazar la reserva.");
        }
    }
}
