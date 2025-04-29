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
    public class BidderRepository : IBidderRepository
    {
        private readonly UsersDbContext _dbContext;

        public BidderRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Bidder bidder)
        {
            await _dbContext.Bidders.AddAsync(bidder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Bidder?> GetByIdAsync(Guid bidderId)
        {
            return await _dbContext.Bidders.FirstOrDefaultAsync(a => a.BidderId == bidderId);
        }

        public async Task DeleteAsync(Guid bidderId)
        {
            var BidderEntity = await _dbContext.Bidders.FindAsync(bidderId);
            if (BidderEntity == null)
            {
                throw new BidderNotFoundException("Bidder not found.");
            }

            _dbContext.Bidders.Remove(BidderEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Bidder?> GetByEmailAsync(String email)
        {
            return await _dbContext.Bidders.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task UpdateAsync(Bidder bidder)
        {
            _dbContext.Bidders.Update(bidder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Bidder>?> GetAllAsync()
        {
            return await _dbContext.Bidders.ToListAsync();
        }
    }
}
