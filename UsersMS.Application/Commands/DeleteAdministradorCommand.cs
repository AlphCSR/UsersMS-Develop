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
    public class DeleteAdministradorCommand : IRequest<String>
    {
        public DeleteAdministradorDto _deleteAdministradorDto { get; set; }
        //public Guid OperatorId { get; set; }
        public DeleteAdministradorCommand(DeleteAdministradorDto deleteAdministradorDto)
        {
            _deleteAdministradorDto = deleteAdministradorDto;
        }
    }
}
