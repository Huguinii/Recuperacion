using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System;

namespace RestAPI.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ReservaEntity>> GetAllAsync()
        {
            return await _context.Reservas.ToListAsync();
        }

        public async Task<ReservaEntity> GetAsync(int id)
        {
            return await _context.Reservas.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservas.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> CreateAsync(ReservaEntity reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ReservaEntity reserva)
        {
            _context.Reservas.Update(reserva);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await GetAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache() { }

        public async Task<ICollection<ReservaEntity>> GetByProfesorAsync(string nombreProfesor)
        {
            return await _context.Reservas
                .Where(r => r.NombreProfesor == nombreProfesor)
                .ToListAsync();
        }

        public async Task<ICollection<ReservaEntity>> GetReservasPendientesAsync()
        {
            return await _context.Reservas
                .Where(r => r.Estado == "Pendiente")
                .ToListAsync();
        }

        public async Task<bool> ExisteReserva(DateOnly fecha, TimeOnly inicio, TimeOnly fin)
        {
            return await _context.Reservas.AnyAsync(r =>
                r.Fecha == fecha &&
                r.Estado != "Rechazada" &&
                (
                    (inicio >= r.HoraInicio && inicio < r.HoraFin) ||
                    (fin > r.HoraInicio && fin <= r.HoraFin) ||
                    (inicio <= r.HoraInicio && fin >= r.HoraFin)
                )
            );
        }
    }

}
