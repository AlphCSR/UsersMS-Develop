using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Operador: User
    {
        public Guid OperadorId { get; set; }
        public Operador(String email, String password, String cedula, String name, String apellido, UserRol rol, UserState state)
        : base(email, password, cedula, name, apellido, rol, state)
        {
            OperadorId = new Guid();
        }

        public Operador() { }
    }
}
