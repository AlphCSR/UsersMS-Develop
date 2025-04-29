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
    public class DeleteBidderCommandHandler : IRequestHandler<DeleteBidderCommand, String>
    {
        private readonly IBidderRepository _BidderRepository;
         
        public DeleteBidderCommandHandler(IBidderRepository BidderRepository)
        {
            _BidderRepository = BidderRepository;
        }

        public async Task<String> Handle(DeleteBidderCommand request, CancellationToken cancellationToken)
        {

            await _BidderRepository.DeleteAsync(request._deleteBidderDto.BidderId);
            return "Bidder eliminado correctamente";
        }
    }
}
