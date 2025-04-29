using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateBidderCommand : IRequest<string>
    {
        public UpdateBidderDto _updateBidderDto { get; set; }

        public UpdateBidderCommand(UpdateBidderDto updateBidderDto)
        {
            _updateBidderDto = updateBidderDto;
        }
    }
}
