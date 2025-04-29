using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateAuctioneerCommand : IRequest<String>
    {
        public CreateAuctioneerDto _createAuctioneerDto { get; set; }

        public CreateAuctioneerCommand(CreateAuctioneerDto createAuctioneerDto)
        {
            _createAuctioneerDto = createAuctioneerDto;
        }
    }
}
