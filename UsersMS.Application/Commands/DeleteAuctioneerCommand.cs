using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class DeleteAuctioneerCommand : IRequest<String>
    {
        public DeleteAuctioneerDto _deleteAuctioneerDto { get; set; }

        public DeleteAuctioneerCommand(DeleteAuctioneerDto deleteAuctioneerDto)
        {
            _deleteAuctioneerDto = deleteAuctioneerDto;
        }
    }
}
