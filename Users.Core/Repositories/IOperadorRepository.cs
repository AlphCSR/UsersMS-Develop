using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IOperadorRepository
    {
        Task<Operador?> GetByIdAsync(Guid operadorId);
        Task<Operador?> GetByEmailAsync(String email);
        Task<List<Operador>?> GetAllAsync();
        Task AddAsync(Operador operador);
        Task DeleteAsync(Guid operadorId);

        Task UpdateAsync(Operador operador);
    }
}
