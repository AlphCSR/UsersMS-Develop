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
    public class ConductorRepository : IConductorRepository
    {
        private readonly UsersDbContext _dbContext;

        public ConductorRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Conductor conductor)
        {
            await _dbContext.Conductores.AddAsync(conductor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Conductor?> GetByIdAsync(Guid ConductorId)
        {
            return await _dbContext.Conductores.FirstOrDefaultAsync(a => a.ConductorId == ConductorId);
        }

        public async Task DeleteAsync(Guid ConductorId)
        {
            var ConductorEntity = await _dbContext.Conductores.FindAsync(ConductorId);
            if (ConductorEntity == null)
            {
                throw new ConductorNotFoundException("Conductor not found.");
            }

            //al parecer cuando elimino no necesito agregar el await para ese metodo en espcifico  
            _dbContext.Conductores.Remove(ConductorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Conductor?> GetByEmailAsync(String email)
        {
            return await _dbContext.Conductores.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task UpdateAsync(Conductor conductor)
        {
            _dbContext.Conductores.Update(conductor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Conductor>?> GetAllAsync()
        {

            return await _dbContext.Conductores.ToListAsync();
        }
    }
}
