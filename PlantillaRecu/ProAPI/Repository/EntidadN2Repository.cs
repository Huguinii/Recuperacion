using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using System;

namespace RestAPI.Repository
{
    public class EntidadN2Repository : IEntidadN2Repository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<EntidadN2Entity> _dbSet;

        public EntidadN2Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<EntidadN2Entity>();
        }

        public async Task<ICollection<EntidadN2Entity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<EntidadN2Entity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CreateAsync(EntidadN2Entity entity)
        {
            await _dbSet.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(EntidadN2Entity entity)
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
