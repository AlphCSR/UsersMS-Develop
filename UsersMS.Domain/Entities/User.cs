using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class User
    {
        //public Guid UserId { get; private set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Cedula { get; set; }

        public string? Name { get; set; }

        public string? Apellido { get; set; }

        public UserRol Rol { get; set; }

        public UserState State { get; set; }

        //llave foranea para el departamento al que pertenece al usuario
        public Guid DepartamentoId { get; set; }

        public User(String email, String password, String cedula, String name, String apellido, UserRol rol, UserState state)
        {

            //UserId = new Guid();
            Email = email;
            Password = password;
            Cedula = cedula;
            Name = name;
            Apellido = apellido;
            Rol = rol;
            State = state;

        }

        public User() { }


    }
}
