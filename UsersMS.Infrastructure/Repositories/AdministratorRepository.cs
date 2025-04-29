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
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly UsersDbContext _dbContext;

        public AdministratorRepository(UsersDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _dbContext.Administrators.AddAsync(administrator);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid AdministradorId)
        {
            var administratorEntity = await _dbContext.Administrators.FindAsync(AdministradorId);
            if (administratorEntity == null)
            {
                throw new AdministratorNotFoundException("Administrator not found.");
            }

            _dbContext.Administrators.Remove(administratorEntity);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Administrator administrator)
        {
            _dbContext.Administrators.Update(administrator);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Administrator?> GetByIdAsync(Guid administradorId)
        {
            return await _dbContext.Administrators.FirstOrDefaultAsync(a => a.AdministratorId == administradorId);
        }

        public async Task<Administrator?> GetByEmailAsync(String email)
        {
            return await _dbContext.Administrators.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<List<Administrator>?> GetAllAsync()
        {
            return await _dbContext.Administrators.ToListAsync();
        }

    }
}
