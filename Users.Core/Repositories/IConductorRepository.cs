using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IConductorRepository
    {
        Task<Conductor?> GetByIdAsync(Guid conductorId);
        Task<Conductor?> GetByEmailAsync(String email);
        Task<List<Conductor>?> GetAllAsync();
        Task AddAsync(Conductor conductor);
        Task DeleteAsync(Guid conductorId);

        Task UpdateAsync(Conductor conductor);
    }
}
