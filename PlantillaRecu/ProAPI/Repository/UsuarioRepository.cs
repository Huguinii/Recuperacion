using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;
using RestAPI.Data;
using RestAPI.Models.DTOs.UserDto;
using Microsoft.EntityFrameworkCore;
using System;

namespace RestAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioEntity?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == id);
        }

        

        public async Task<bool> CreateAsync(UsuarioEntity usuario)
        {
            if (await ExistsAsync(usuario.Id))
                return false;

            await _context.Usuarios.AddAsync(usuario);
            return await Save();
        }

        public async Task<ICollection<UsuarioEntity>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public Task<UsuarioEntity> GetAsync(int id)
        {
            throw new NotSupportedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotSupportedException();
        }

        public Task<bool> UpdateAsync(UsuarioEntity entity)
        {
            _context.Usuarios.Update(entity);
            return Save();
        }

        public Task<bool> Save()
        {
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public void ClearCache()
        {
            throw new NotSupportedException();
        }

        
    }

}
