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
    public class GetAllAuctioneersQueryHandler : IRequestHandler<GetAllAuctioneersQuery, List<GetAllAuctioneersDto>>
    {
        private readonly IAuctioneerRepository _AuctioneerRepository;

        public GetAllAuctioneersQueryHandler(IAuctioneerRepository AuctioneerRepository)
        {
            _AuctioneerRepository = AuctioneerRepository;
        }

        public async Task<List<GetAllAuctioneersDto>> Handle(GetAllAuctioneersQuery request, CancellationToken cancellationToken)
        {
            var Auctioneers = await _AuctioneerRepository.GetAllAsync();

            if (Auctioneers == null)
            {
                throw new AuctioneerNotFoundException("Auctioneers not found.");
            }
            else
            {
                var AuctioneersDto = Auctioneers.Select(o => new GetAllAuctioneersDto
                {
                    AuctioneerId = o.AuctioneerId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Id = o.Id!,
                    Name = o.Name!,
                    LastName = o.LastName!,
                    Role = o.Role!,
                    Phone = o.Phone!,
                    Address = o.Address!,
                    State = o.State!,
                }).ToList();

                return AuctioneersDto;
            }
        }
    }
}
