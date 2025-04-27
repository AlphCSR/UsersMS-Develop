using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IProveedorRepository
    {
        Task<Proveedor?> GetByIdAsync(Guid proveedorId);
        Task<Proveedor?> GetByEmailAsync(String email);
        Task<List<Proveedor>?> GetAllAsync();
        Task AddAsync(Proveedor proveedor);
        Task DeleteAsync(Guid proveedorId);

        Task UpdateAsync(Proveedor proveedor);
    }
}
