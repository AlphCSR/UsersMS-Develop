using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateAdministradorCommand : IRequest<string>
    {
        public UpdateAdministradorDto _updateAdministradorDto { get; set; }

        public UpdateAdministradorCommand(UpdateAdministradorDto updateAdministradorDto)
        {
            _updateAdministradorDto = updateAdministradorDto;
        }
    }
}
