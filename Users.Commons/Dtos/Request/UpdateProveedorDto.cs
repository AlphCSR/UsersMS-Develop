using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Commons.Dtos.Request
{
    public record UpdateProveedorDto
    {
        public Guid ProveedorId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Cedula { get; set; }

        public string? Name { get; set; }

        public string? Apellido { get; set; }

        public UserRol? Rol { get; set; }

        public UserState? State { get; set; }

        public Guid? DepartamentoId { get; set; }

        public Guid? EmpresaId { get; set; }
    }
}
