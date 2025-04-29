using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Commons.Dtos.Request
{
    public record DeleteTechnicalSupportDto
    {
        public Guid TechnicalSupportId { get; set; }
    }
}
