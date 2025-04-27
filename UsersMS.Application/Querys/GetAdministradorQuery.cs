using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetAdministradorQuery : IRequest<GetAdministradorDto>
    {
        public Guid AdministradorId { get; set; }

        public GetAdministradorQuery(Guid administradorId)
        {
            AdministradorId = administradorId;
        }
    }
}
