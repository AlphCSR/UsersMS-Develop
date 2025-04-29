using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Exceptions
{
    public class AdministratorNotFoundException : Exception
    {
        public AdministratorNotFoundException() { }

        public AdministratorNotFoundException(string message)
            : base(message) { }

        public AdministratorNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
