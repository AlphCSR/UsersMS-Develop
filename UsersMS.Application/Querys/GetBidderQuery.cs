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
    public class GetBidderQuery : IRequest<GetBidderDto>
    {
        public Guid BidderId { get; set; }

        public GetBidderQuery(Guid bidderId)
        {
            BidderId = bidderId;
        }
    }
}
