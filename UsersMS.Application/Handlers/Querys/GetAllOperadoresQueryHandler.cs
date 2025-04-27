using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Response;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Querys
{
    public class GetAllOperadoresQueryHandler : IRequestHandler<GetAllOperadoresQuery, List<GetAllOperadoresDto>>
    {
        private readonly IOperadorRepository _operadorRepository;

        public GetAllOperadoresQueryHandler(IOperadorRepository operadorRepository)
        {
            _operadorRepository = operadorRepository;
        }

        public async Task<List<GetAllOperadoresDto>> Handle(GetAllOperadoresQuery request, CancellationToken cancellationToken)
        {
            var Operadores = await _operadorRepository.GetAllAsync();

            if (Operadores == null)
            {
                throw new OperadorNotFoundException("Operadores not found.");

            }
            else
            {
                //como el handler debe retonar una lista de dto correspondiente se deben mapear los datos de las entidades a dto
                var OperadoresDto = Operadores.Select(o => new GetAllOperadoresDto
                {
                    OperadorId = o.OperadorId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Cedula = o.Cedula!,
                    Name = o.Name!,
                    Apellido = o.Apellido!,
                    Rol = o.Rol!,
                    State = o.State!,
                    DepartamentoId = o.DepartamentoId!,
                }).ToList();

                return OperadoresDto;


            }
        }
    }
}
