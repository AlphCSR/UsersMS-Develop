using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class CreateAdministratorCommand : IRequest<String>
    {
        public CreateAdministratorDto _createAdministratorDto  { get; set; }

        public CreateAdministratorCommand(CreateAdministratorDto createAdministratorDto)
        {
            _createAdministratorDto = createAdministratorDto;
        }
    }
}
