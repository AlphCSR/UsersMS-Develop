using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Commons.Dtos.Response
{
    public record GetAllConductoresDto
    {
        public Guid ConductorId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public UserRol Rol { get; set; }

        public UserState State { get; set; }
        public bool Licencia { get; set; }

        public bool CertificadoSalud { get; set; }
        public Guid DepartamentoId { get; set; }
        public Guid? EmpresaId { get; set; }

        public Guid? GruaId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
