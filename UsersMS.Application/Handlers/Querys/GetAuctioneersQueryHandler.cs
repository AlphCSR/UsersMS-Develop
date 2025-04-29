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
    public class GetAuctioneersQueryHandler : IRequestHandler<GetAuctioneerQuery, GetAuctioneerDto>
    {
        private readonly IAuctioneerRepository _AuctioneerRepository;

        public GetAuctioneersQueryHandler(IAuctioneerRepository AuctioneerRepository)
        {
            _AuctioneerRepository = AuctioneerRepository;
        }

        public async Task<GetAuctioneerDto> Handle(GetAuctioneerQuery request, CancellationToken cancellationToken)
        {
            var AuctioneerEntity = await _AuctioneerRepository.GetByIdAsync(request.AuctioneerId);

            if (AuctioneerEntity == null)
                throw new AuctioneerNotFoundException("Auctioneer not found.");

            return new GetAuctioneerDto
            {
                AuctioneerId = AuctioneerEntity.AuctioneerId,
                Email = AuctioneerEntity.Email!,
                Password = AuctioneerEntity.Password!,
                Id = AuctioneerEntity.Id!,
                Name = AuctioneerEntity.Name!,
                LastName = AuctioneerEntity.LastName!,
                Role = AuctioneerEntity.Role!,
                Phone = AuctioneerEntity.Phone!,
                Address = AuctioneerEntity.Address!,
                State = AuctioneerEntity.State!,
            };
        }
    }
}
