using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateAuctioneerCommand : IRequest<string>
    {
        public UpdateAuctioneerDto _updateAuctioneerDto { get; set; }

        public UpdateAuctioneerCommand(UpdateAuctioneerDto updateAuctioneerDto)
        {
            _updateAuctioneerDto = updateAuctioneerDto;
        }
    }
}
