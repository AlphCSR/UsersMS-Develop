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
    public class AuctioneerRepository : IAuctioneerRepository
    {
        private readonly UsersDbContext _dbContext;

        public AuctioneerRepository(UsersDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task AddAsync(Auctioneer auctioneer)
        {
            await _dbContext.Auctioneers.AddAsync(auctioneer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Auctioneer?> GetByIdAsync(Guid operadorId)
        {
            return await _dbContext.Auctioneers.FirstOrDefaultAsync(a => a.AuctioneerId == operadorId);
        }

        public async Task<Auctioneer?> GetByEmailAsync(String email)
        {
            return await _dbContext.Auctioneers.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task DeleteAsync(Guid auctioneerId)
        {
            var operadorEntity = await _dbContext.Auctioneers.FindAsync(auctioneerId);
            if (operadorEntity == null)
            {
                throw new AuctioneerNotFoundException("Auctioneer not found.");
            }

            _dbContext.Auctioneers.Remove(operadorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auctioneer auctioneer)
        {
            _dbContext.Auctioneers.Update(auctioneer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Auctioneer>?> GetAllAsync()
        {

            return await _dbContext.Auctioneers.ToListAsync();
        }
    }
}
