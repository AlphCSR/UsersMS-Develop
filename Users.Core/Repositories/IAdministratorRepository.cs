using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IAdministratorRepository
    {
        Task<Administrator?> GetByIdAsync(Guid administratorId);
        Task<Administrator?> GetByEmailAsync(String email);
        Task<List<Administrator>?> GetAllAsync();
        Task AddAsync(Administrator administrator);
        Task DeleteAsync(Guid administratorId);
        Task UpdateAsync(Administrator administrator);
    }
}
