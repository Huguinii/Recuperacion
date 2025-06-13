using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System;

namespace RestAPI.Repository
{
    public class DiaRepository : IDiaRepository
    {
        private readonly ApplicationDbContext _context;

        public DiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<DiaEntity>> GetAllAsync()
        {
            return await _context.DiasNoLectivos.ToListAsync();
        }

        public async Task<DiaEntity> GetAsync(int id)
        {
            return await _context.DiasNoLectivos.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DiasNoLectivos.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> CreateAsync(DiaEntity entity)
        {
            await _context.DiasNoLectivos.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(DiaEntity entity)
        {
            _context.DiasNoLectivos.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _context.DiasNoLectivos.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache() { }

        public async Task<bool> EsDiaNoLectivo(DateTime fecha)
        {
            return await _context.DiasNoLectivos.AnyAsync(d => d.Fecha.Date == fecha.Date);
        }
    }


}
