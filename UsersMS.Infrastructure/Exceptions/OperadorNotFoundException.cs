using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Infrastructure.Exceptions
{
    public class OperadorNotFoundException : Exception
    {
        public OperadorNotFoundException() { }

        public OperadorNotFoundException(string message)
            : base(message) { }

        public OperadorNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
