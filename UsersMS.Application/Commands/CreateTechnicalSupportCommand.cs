using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateTechnicalSupportCommand : IRequest<String>
    {
        public CreateTechnicalSupportDto _createTechnicalSupportDto { get; set; }

        public CreateTechnicalSupportCommand(CreateTechnicalSupportDto createTechnicalSupportDto)
        {
            _createTechnicalSupportDto = createTechnicalSupportDto;
        }
    }
}
