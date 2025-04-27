using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    //Unit para devolver nada
    public class DeleteOperadorCommand : IRequest<String>
    {
        public DeleteOperadorDto _deleteOperadorDto { get; set; }

        //public Guid OperatorId { get; set; }
        public DeleteOperadorCommand(DeleteOperadorDto deleteOperadorDto)
        {
            _deleteOperadorDto = deleteOperadorDto;
        }
    }
}
