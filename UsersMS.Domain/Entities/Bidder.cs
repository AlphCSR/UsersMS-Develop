using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Bidder: User
    {
        public Guid BidderId { get; set; }

        public Bidder(String email, String password, String id, String name, String lastname, String phone, String address, UserRole rol, UserState state)
        : base(email, password, id, name, lastname, phone, address, rol, state)
        {
            BidderId = new Guid();
        }
        public Bidder() { }
    }
}
