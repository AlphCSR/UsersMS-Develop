using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class DeleteConductorCommand : IRequest<String>
    {
        public DeleteConductorDto _deleteConductorDto { get; set; }

        //public Guid OperatorId { get; set; }
        public DeleteConductorCommand(DeleteConductorDto deleteConductorDto)
        {
            _deleteConductorDto = deleteConductorDto;
        }
    }
}
