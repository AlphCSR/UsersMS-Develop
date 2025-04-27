using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateConductorCommand : IRequest<String>
    {
        public CreateConductorDto _createConductorDto { get; set; }

        public CreateConductorCommand(CreateConductorDto createConductorDto)
        {
            _createConductorDto = createConductorDto;
        }
    }
}
