using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetProveedorQuery : IRequest<GetProveedorDto>
    {
        public Guid ProveedorId { get; set; }

        public GetProveedorQuery(Guid proveedorId)
        {
            ProveedorId = proveedorId;
        }
    }
}
