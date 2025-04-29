using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class DeleteBidderCommand : IRequest<String>
    {
        public DeleteBidderDto _deleteBidderDto { get; set; }

        public DeleteBidderCommand(DeleteBidderDto deleteBidderDto)
        {
            _deleteBidderDto = deleteBidderDto;
        }
    }
}
