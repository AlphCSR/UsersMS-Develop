using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IAuctioneerRepository
    {
        Task<Auctioneer?> GetByIdAsync(Guid auctioneerId);
        Task<Auctioneer?> GetByEmailAsync(String email);
        Task<List<Auctioneer>?> GetAllAsync();
        Task AddAsync(Auctioneer auctioneer);
        Task DeleteAsync(Guid auctioneerId);
        Task UpdateAsync(Auctioneer auctioneer);
    }
}
