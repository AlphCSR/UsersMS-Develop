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
    public class TechnicalSupportRepository: ITechnicalSupportRepository
    {
        private readonly UsersDbContext _dbContext;

        public TechnicalSupportRepository(UsersDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task AddAsync(TechnicalSupport technicalSupport)
        {
            await _dbContext.TechnicalSupports.AddAsync(technicalSupport);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TechnicalSupport?> GetByIdAsync(Guid technicalSupportId)
        {
            return await _dbContext.TechnicalSupports.FirstOrDefaultAsync(a => a.TechnicalSupportId == technicalSupportId);
        }

        public async Task DeleteAsync(Guid technicalSupportId)
        {
            var TechnicalSupportEntity = await _dbContext.TechnicalSupports.FindAsync(technicalSupportId);
            if (TechnicalSupportEntity == null)
            {
                throw new TechnicalSupportNotFoundException("TechnicalSupport not found.");
            }

            _dbContext.TechnicalSupports.Remove(TechnicalSupportEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TechnicalSupport technicalSupport)
        {
            _dbContext.TechnicalSupports.Update(technicalSupport);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TechnicalSupport?> GetByEmailAsync(String email)
        {
            return await _dbContext.TechnicalSupports.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task<List<TechnicalSupport>?> GetAllAsync()
        {
            return await _dbContext.TechnicalSupports.ToListAsync();
        }
        
    }
}
