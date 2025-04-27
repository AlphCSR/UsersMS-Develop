using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateAdministradorCommand : IRequest<String>
    {
        public CreateAdministradorDto _createAdministradorDto  { get; set; }

        public CreateAdministradorCommand(CreateAdministradorDto createAdministradorDto)
        {
            _createAdministradorDto = createAdministradorDto;
        }
    }
}
