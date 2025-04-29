using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersMS.Commons.Dtos.Request
{
    public record DeleteAuctioneerDto
    {
        public Guid AuctioneerId { get; set; }
    }
}
