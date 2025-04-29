using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;
using UsersMS.Domain.Entities;

namespace UsersMS.Application.Querys
{
    public class GetAuctioneerQuery : IRequest<GetAuctioneerDto>
    {
        public Guid AuctioneerId { get; set; }

        public GetAuctioneerQuery(Guid auctioneerId)
        {
            AuctioneerId = auctioneerId;
        }
    }
}
