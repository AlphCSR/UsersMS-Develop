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
    public class OperadorRepository : IOperadorRepository
    {
        private readonly UsersDbContext _dbContext;

        public OperadorRepository(UsersDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task AddAsync(Operador operador)
        {
            await _dbContext.Operadores.AddAsync(operador);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Operador?> GetByIdAsync(Guid operadorId)
        {
            return await _dbContext.Operadores.FirstOrDefaultAsync(a => a.OperadorId == operadorId);
        }

        public async Task<Operador?> GetByEmailAsync(String email)
        {
            return await _dbContext.Operadores.FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task DeleteAsync(Guid operadorId)
        {
            var operadorEntity = await _dbContext.Operadores.FindAsync(operadorId);
            if (operadorEntity == null)
            {
                throw new OperadorNotFoundException("Operador not found.");
            }

            //al parecer cuando elimino no necesito agregar el await para ese metodo en espcifico  
            _dbContext.Operadores.Remove(operadorEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Operador operador)
        {
            _dbContext.Operadores.Update(operador);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Operador>?> GetAllAsync()
        {

            return await _dbContext.Operadores.ToListAsync();
        }
    }
}
