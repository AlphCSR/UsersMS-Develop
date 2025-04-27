using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Administrador : User
    {
        public Guid AdministradorId { get; set; }
        //Referencia a la emrpesa. Tipo administrador para la emrpesa Gruas Ucab y tipo Proveedor para proveedores externos
        public Guid? EmpresaId { get; set; }
        public Administrador(String email, String password, String cedula, String name, String apellido, UserRol rol, UserState state)
        : base(email, password, cedula, name, apellido, rol, state)
        {
            AdministradorId = new Guid();
        
        }

        public Administrador() { }

    }
}
