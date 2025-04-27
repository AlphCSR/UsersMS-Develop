using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Exceptions
{
    public class ProveedorNotFoundException : Exception
    {
        public ProveedorNotFoundException() { }

        public ProveedorNotFoundException(string message)
            : base(message) { }

        public ProveedorNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
