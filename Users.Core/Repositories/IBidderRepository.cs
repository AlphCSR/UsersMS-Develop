using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IBidderRepository
    {
        Task<Bidder?> GetByIdAsync(Guid bidderId);
        Task<Bidder?> GetByEmailAsync(String email);
        Task<List<Bidder>?> GetAllAsync();
        Task AddAsync(Bidder bidder);
        Task DeleteAsync(Guid bidderId);
        Task UpdateAsync(Bidder bidder);
    }
}
