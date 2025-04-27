using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Proveedor : User
    {
        public Guid ProveedorId { get; set; }
        //llave foranea a emrpesa
        public Guid EmpresaId { get; set; }
        public Proveedor(String email, String password, String cedula, String name, String apellido, UserRol rol, UserState state)
        : base(email, password, cedula, name, apellido, rol, state)
        {
            ProveedorId = new Guid();
        }

        public Proveedor() { }
    }
}
