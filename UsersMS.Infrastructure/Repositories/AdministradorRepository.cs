using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;
using UsersMS.Infrastructure.DataBase;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Infrastructure.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly UsersDbContext _dbContext;

        public AdministradorRepository(UsersDbContext dbContext) 
        {
            _dbContext = dbContext;
        
        }

        public async Task AddAsync(Administrador administrador)
        {
            await _dbContext.Administradores.AddAsync(administrador);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid AdministradorId)
        {
            var administradorEntity = await _dbContext.Administradores.FindAsync(AdministradorId);
            if (administradorEntity == null)
            {
                throw new AdministradorNotFoundException("Administrador not found.");
            }

            //al parecer cuando elimino no necesito agregar el await para ese metodo en espcifico  
            _dbContext.Administradores.Remove(administradorEntity);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Administrador administrador)
        {
            _dbContext.Administradores.Update(administrador);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Administrador?> GetByIdAsync(Guid administradorId)
        {
            return await _dbContext.Administradores.FirstOrDefaultAsync(a => a.AdministradorId == administradorId);
        }

        public async Task<Administrador?> GetByEmailAsync(String email)
        {
            return await _dbContext.Administradores.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<List<Administrador>?> GetAllAsync()
        {

            return await _dbContext.Administradores.ToListAsync();
        }

    }
}
