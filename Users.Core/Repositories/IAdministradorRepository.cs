using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Core.Repositories
{
    public interface IAdministradorRepository
    {
        Task<Administrador?> GetByIdAsync(Guid administradorId);

        Task<List<Administrador>?> GetAllAsync();
        Task AddAsync(Administrador administrador);
        Task DeleteAsync(Guid administradorId);

        Task UpdateAsync(Administrador administrador);

        Task<Administrador?> GetByEmailAsync(String email);
    }
}
