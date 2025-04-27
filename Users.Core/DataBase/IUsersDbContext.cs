using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;
namespace UsersMS.Core.DataBase
{
    public interface IUsersDbContext
    {
        DbContext DbContext { get; }

        //DbSet<User> Users { get; set; }
        DbSet<Administrador> Administradores { get; set; }
        DbSet<Operador> Operadores { get; set; }

        DbSet<Proveedor> Proveedores { get; set; }
        DbSet<Conductor> Conductores { get; set; }

        IDbContextTransactionProxy BeginTransaction();

        void ChangeEntityState<TEntity>(TEntity entity, EntityState state);

        Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default);
    }
}
