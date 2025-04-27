using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateOperadorCommand : IRequest<string>
    {
        public UpdateOperadorDto _updateOperadorDto { get; set; }

        public UpdateOperadorCommand(UpdateOperadorDto updateOperadorDto)
        {
            _updateOperadorDto = updateOperadorDto;
        }
    }
}
