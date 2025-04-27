using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateConductorCommand : IRequest<string>
    {
        public UpdateConductorDto _updateConductorDto { get; set; }

        public UpdateConductorCommand(UpdateConductorDto updateConductorDto)
        {
            _updateConductorDto = updateConductorDto;
        }
    }
}
