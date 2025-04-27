using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Exceptions
{
    public class ConductorNotFoundException : Exception
    {
        public ConductorNotFoundException() { }

        public ConductorNotFoundException(string message)
            : base(message) { }

        public ConductorNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
