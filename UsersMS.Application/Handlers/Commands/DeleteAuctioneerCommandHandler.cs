using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;

namespace UsersMS.Application.Handlers.Commands
{
    public class DeleteAuctioneerCommandHandler : IRequestHandler<DeleteAuctioneerCommand, String>
    {
        private readonly IAuctioneerRepository _AuctioneerRepository;

        public DeleteAuctioneerCommandHandler(IAuctioneerRepository AuctioneerRepository)
        {
            _AuctioneerRepository = AuctioneerRepository;
        }

        public async Task<String> Handle(DeleteAuctioneerCommand request, CancellationToken cancellationToken)
        {
            await _AuctioneerRepository.DeleteAsync(request._deleteAuctioneerDto.AuctioneerId);
            return "Auctioneer eliminado correctamente";
        }
    }
}
