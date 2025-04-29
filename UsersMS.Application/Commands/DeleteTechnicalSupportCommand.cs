using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Request;

namespace UsersMS.Application.Commands
{
    public class DeleteTechnicalSupportCommand : IRequest<String>
    {
        public DeleteTechnicalSupportDto _deleteTechnicalSupportDto { get; set; }

        public DeleteTechnicalSupportCommand(DeleteTechnicalSupportDto deleteTechnicalSupportDto)
        {
            _deleteTechnicalSupportDto = deleteTechnicalSupportDto;
        }
    }
}
