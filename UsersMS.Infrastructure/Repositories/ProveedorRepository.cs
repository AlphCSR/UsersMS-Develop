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
    public class ProveedorRepository: IProveedorRepository
    {
        private readonly UsersDbContext _dbContext;

        public ProveedorRepository(UsersDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task AddAsync(Proveedor proveedor)
        {
            await _dbContext.Proveedores.AddAsync(proveedor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(Guid proveedorId)
        {
            return await _dbContext.Proveedores.FirstOrDefaultAsync(a => a.ProveedorId == proveedorId);
        }

        public async Task DeleteAsync(Guid ProveedorId)
        {
            var ProveedorEntity = await _dbContext.Proveedores.FindAsync(ProveedorId);
            if (ProveedorEntity == null)
            {
                throw new ProveedorNotFoundException("Proveedor not found.");
            }

            //al parecer cuando elimino no necesito agregar el await para ese metodo en espcifico  
            _dbContext.Proveedores.Remove(ProveedorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Proveedor Proveedor)
        {
            _dbContext.Proveedores.Update(Proveedor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Proveedor?> GetByEmailAsync(String email)
        {
            return await _dbContext.Proveedores.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task<List<Proveedor>?> GetAllAsync()
        {

            return await _dbContext.Proveedores.ToListAsync();
        }
        
    }
}
