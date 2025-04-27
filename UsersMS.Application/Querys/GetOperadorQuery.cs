using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Commons.Dtos.Response;

namespace UsersMS.Application.Querys
{
    public class GetOperadorQuery : IRequest<GetOperadorDto>
    {
        public Guid OperadorId { get; set; }

        public GetOperadorQuery(Guid operadorId)
        {
            OperadorId = operadorId;
        }
    }
}
