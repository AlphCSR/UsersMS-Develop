using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Domain.Entities;

namespace UsersMS.Infrastructure.DataBase.Configuration
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.Property(s => s.OperadorId).IsRequired();
            builder.Property(s => s.Email).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Password).IsRequired();
            builder.Property(s => s.Cedula).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Apellido).IsRequired();
            builder.Property(s => s.Rol).IsRequired();
            builder.Property(s => s.State).IsRequired();
            builder.Property(s => s.DepartamentoId);//al no colocarle nada estoy diciendo que no son requeridos, me permite crear un administrador sin tener que darle estos atributos,
            //lo que me permite pasarlos luego utilizando el cliente
        }
    }
}
