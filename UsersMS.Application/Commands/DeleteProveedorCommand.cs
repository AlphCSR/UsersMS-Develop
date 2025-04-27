using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class DeleteProveedorCommand : IRequest<String>
    {
        public DeleteProveedorDto _deleteProveedorDto { get; set; }

        //public Guid OperatorId { get; set; }
        public DeleteProveedorCommand(DeleteProveedorDto deleteProveedorDto)
        {
            _deleteProveedorDto = deleteProveedorDto;
        }
    }
}
