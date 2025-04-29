using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetTechnicalSupportQuery : IRequest<GetTechnicalSupportDto>
    {
        public Guid TechnicalSupportId { get; set; }

        public GetTechnicalSupportQuery(Guid technicalSupportId)
        {
            TechnicalSupportId = technicalSupportId;
        }
    }
}
