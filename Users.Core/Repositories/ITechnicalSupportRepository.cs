using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface ITechnicalSupportRepository
    {
        Task<TechnicalSupport?> GetByIdAsync(Guid technicalSupportId);
        Task<TechnicalSupport?> GetByEmailAsync(String email);
        Task<List<TechnicalSupport>?> GetAllAsync();
        Task AddAsync(TechnicalSupport technicalSupport);
        Task DeleteAsync(Guid technicalSupportId);
        Task UpdateAsync(TechnicalSupport technicalSupport);
    }
}
