using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Administrator : User
    {
        public Guid AdministratorId { get; set; }

        public Administrator(String email, String password, String id, String name, String lastname, String phone, String address, UserRole rol, UserState state)
        : base(email, password, id, name, lastname, phone, address, rol, state)
        {
            AdministratorId = new Guid();
        }

        public Administrator() { }

    }
}
