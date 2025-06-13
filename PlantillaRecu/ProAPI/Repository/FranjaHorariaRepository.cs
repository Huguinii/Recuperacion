using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System;

namespace RestAPI.Repository
{
    public class FranjaHorariaRepository : IFranjaHorariaRepository
    {
        private readonly ApplicationDbContext _context;

        public FranjaHorariaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<FranjaHorariaEntity>> GetAllAsync()
        {
            return await _context.FranjasHorarias.ToListAsync();
        }

        public async Task<FranjaHorariaEntity> GetAsync(int id)
        {
            return await _context.FranjasHorarias.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.FranjasHorarias.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> CreateAsync(FranjaHorariaEntity entity)
        {
            await _context.FranjasHorarias.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(FranjaHorariaEntity entity)
        {
            _context.FranjasHorarias.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.FranjasHorarias.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache() { }

        public async Task<ICollection<FranjaHorariaEntity>> GetDisponiblesAsync()
        {
            return await _context.FranjasHorarias
                .Where(f => f.Disponible && !f.EsRecreo)
                .ToListAsync();
        }
    }



}
