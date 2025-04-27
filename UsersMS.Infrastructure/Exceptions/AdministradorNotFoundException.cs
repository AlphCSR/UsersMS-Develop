using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Exceptions
{
    public class AdministradorNotFoundException : Exception
    {
        public AdministradorNotFoundException() { }

        public AdministradorNotFoundException(string message)
            : base(message) { }

        public AdministradorNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
