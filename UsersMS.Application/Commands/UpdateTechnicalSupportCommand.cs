using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class UpdateTechnicalSupportCommand : IRequest<string>
    {
        public UpdateTechnicalSupportDto _updateTechnicalSupportDto { get; set; }

        public UpdateTechnicalSupportCommand(UpdateTechnicalSupportDto updateTechnicalSupportDto)
        {
            _updateTechnicalSupportDto = updateTechnicalSupportDto;
        }
    }
}
