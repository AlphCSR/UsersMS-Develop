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
    public class DeleteAdministratorCommand : IRequest<String>
    {
        public DeleteAdministratorDto _deleteAdministratorDto { get; set; }
        public DeleteAdministratorCommand(DeleteAdministratorDto deleteAdministratorDto)
        {
            _deleteAdministratorDto = deleteAdministratorDto;
        }
    }
}
