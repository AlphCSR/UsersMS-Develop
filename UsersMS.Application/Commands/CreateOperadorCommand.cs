using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateOperadorCommand : IRequest<String>
    {
        public CreateOperadorDto _createOperadorDto { get; set; }

        public CreateOperadorCommand(CreateOperadorDto createOperadorDto)
        {
            _createOperadorDto = createOperadorDto;
        }
    }
}
