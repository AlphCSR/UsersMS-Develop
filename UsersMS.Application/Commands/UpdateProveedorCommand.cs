using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateProveedorCommand : IRequest<string>
    {
        public UpdateProveedorDto _updateProveedorDto { get; set; }

        public UpdateProveedorCommand(UpdateProveedorDto updateProveedorDto)
        {
            _updateProveedorDto = updateProveedorDto;
        }
    }
}
