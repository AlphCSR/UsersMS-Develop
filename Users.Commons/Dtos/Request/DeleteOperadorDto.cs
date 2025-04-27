using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Commons.Dtos.Request
{
    public record DeleteOperadorDto
    {
        public Guid OperadorId { get; set; }
    }
}
