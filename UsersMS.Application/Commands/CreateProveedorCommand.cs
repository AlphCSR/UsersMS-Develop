using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateProveedorCommand : IRequest<String>
    {
        public CreateProveedorDto _createProveedorDto { get; set; }

        public CreateProveedorCommand(CreateProveedorDto createProveedorDto)
        {
            _createProveedorDto = createProveedorDto;
        }
    }
}
