using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Response;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;
using UsersMS.Infrastructure.Repositories;

namespace UsersMS.Application.Handlers.Querys
{
    public class GetAllBiddersQueryHandler : IRequestHandler<GetAllBiddersQuery, List<GetAllBiddersDto>>
    {
        private readonly IBidderRepository _BidderRepository;

        public GetAllBiddersQueryHandler(IBidderRepository BidderRepository)
        {
            _BidderRepository = BidderRepository;
        }

        public async Task<List<GetAllBiddersDto>> Handle(GetAllBiddersQuery request, CancellationToken cancellationToken)
        {
            var Bidder = await _BidderRepository.GetAllAsync();

            if (Bidder == null)
            {
                throw new BidderNotFoundException("Bidders not found.");

            }
            else
            {
                var BiddersDto = Bidder.Select(o => new GetAllBiddersDto
                {
                    BidderId = o.BidderId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Id = o.Id!,
                    Name = o.Name!,
                    LastName = o.LastName!,
                    Phone = o.Phone!,
                    Address = o.Address!,
                    Role = o.Role!,
                    State = o.State!,
                }).ToList();

                return BiddersDto;
            }
        }
    }
}
