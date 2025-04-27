using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Conductor : User
    {  
        public Guid ConductorId { get; set; }
        public bool Licencia { get; set; }

        public bool CertificadoSalud { get; set; }
        
        //Llave foranea a empresa
        public Guid? EmpresaId { get; set; }
        //Llave foranea a grua
        public Guid? GruaId { get; set; }

        public Conductor(String email, String password, String cedula, String name, String apellido, UserRol rol, UserState state, bool licencia, bool certificadoSalud)
        :base(email,password,cedula,name,apellido,rol,state)
        {
            ConductorId = new Guid();
            Licencia = licencia;
            CertificadoSalud = certificadoSalud;
        }

        public Conductor() { }
    }
}
