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

namespace UsersMS.Application.Handlers.Querys
{
    public class GetBidderQueryHandler : IRequestHandler<GetBidderQuery, GetBidderDto>
    {
        private readonly IBidderRepository _BidderRepository;

        public GetBidderQueryHandler(IBidderRepository BidderRepository)
        {
            _BidderRepository = BidderRepository;
        }

        public async Task<GetBidderDto> Handle(GetBidderQuery request, CancellationToken cancellationToken)
        {
            var BidderEntity = await _BidderRepository.GetByIdAsync(request.BidderId);

            if (BidderEntity == null)
                throw new BidderNotFoundException("Bidder not found.");

            //mapeando de la entidad al dto - Cliente recibe dto no entidades
            return new GetBidderDto
            {
                BidderId = BidderEntity.BidderId,
                Email = BidderEntity.Email!,
                Password = BidderEntity.Password!,
                Id = BidderEntity.Id!,
                Name = BidderEntity.Name!,
                LastName = BidderEntity.LastName!,
                Role = BidderEntity.Role!,
                Phone = BidderEntity.Phone!,
                Address = BidderEntity.Address!,
                State = BidderEntity.State!,
            };
        }
    }
}
