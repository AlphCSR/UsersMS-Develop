using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateAdministratorCommand : IRequest<string>
    {
        public UpdateAdministratorDto _updateAdministratorDto { get; set; }

        public UpdateAdministratorCommand(UpdateAdministratorDto updateAdministratorDto)
        {
            _updateAdministratorDto = updateAdministratorDto;
        }
    }
}
