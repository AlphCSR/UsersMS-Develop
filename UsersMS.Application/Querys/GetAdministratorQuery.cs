using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetAdministratorQuery : IRequest<GetAdministratorDto>
    {
        public Guid AdministradorId { get; set; }

        public GetAdministratorQuery(Guid administradorId)
        {
            AdministradorId = administradorId;
        }
    }
}
