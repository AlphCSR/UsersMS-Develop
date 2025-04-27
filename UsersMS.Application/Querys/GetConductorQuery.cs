using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetConductorQuery : IRequest<GetConductorDto>
    {
        public Guid ConductorId { get; set; }

        public GetConductorQuery(Guid conductorId)
        {
            ConductorId = conductorId;
        }
    }
}
