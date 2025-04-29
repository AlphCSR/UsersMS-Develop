using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Enums;

namespace UsersMS.Domain.Entities
{
    public class Auctioneer : User
    {  
        public Guid AuctioneerId { get; set; }

        public Auctioneer(String email, String password, String id, String name, String lastname, String phone, String address, UserRole rol, UserState state)
        : base(email, password, id, name, lastname, phone, address, rol, state)
        {
            AuctioneerId = new Guid();
        }
        public Auctioneer() { }
    }
}
