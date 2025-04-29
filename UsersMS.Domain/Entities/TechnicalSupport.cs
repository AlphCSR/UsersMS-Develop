using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class TechnicalSupport : User
    {
        public Guid TechnicalSupportId { get; set; }

        public TechnicalSupport(String email, String password, String id, String name, String lastname, String phone, String address, UserRole rol, UserState state)
        : base(email, password, id, name, lastname, phone, address, rol, state)
        {
            TechnicalSupportId = new Guid();
        }
        public TechnicalSupport() { }
    }
}
