using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System;

namespace RestAPI.Repository
{
    public class EntidadN1Repository : IEntidadN1Repository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<EntidadN1Entity> _dbSet;

        public EntidadN1Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EntidadN1Entity>();
        }

        public async Task<ICollection<EntidadN1Entity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<EntidadN1Entity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CreateAsync(EntidadN1Entity entity)
        {
            await _dbSet.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(EntidadN1Entity entity)
        {
            _dbSet.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache(){ }
    }


}
