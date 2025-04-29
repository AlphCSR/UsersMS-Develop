using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateBidderCommand : IRequest<String>
    {
        public CreateBidderDto _createBidderDto { get; set; }

        public CreateBidderCommand(CreateBidderDto createBidderDto)
        {
            _createBidderDto = createBidderDto;
        }
    }
}
