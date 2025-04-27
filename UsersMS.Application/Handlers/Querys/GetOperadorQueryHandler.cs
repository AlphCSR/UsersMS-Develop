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
    public class GetOperadorQueryHandler : IRequestHandler<GetOperadorQuery, GetOperadorDto>
    {
        private readonly IOperadorRepository _OperadorRepository;

        public GetOperadorQueryHandler(IOperadorRepository OperadorRepository)
        {
            _OperadorRepository = OperadorRepository;
        }

        public async Task<GetOperadorDto> Handle(GetOperadorQuery request, CancellationToken cancellationToken)
        {
            var OperadorEntity = await _OperadorRepository.GetByIdAsync(request.OperadorId);

            if (OperadorEntity == null)
                throw new OperadorNotFoundException("Operador not found.");

            //mapeando de la entidad al dto - Cliente recibe dto no entidades
            return new GetOperadorDto
            {
                OperadorId = OperadorEntity.OperadorId,
                Email = OperadorEntity.Email!,
                Password = OperadorEntity.Password!,
                Cedula = OperadorEntity.Cedula!,
                Name = OperadorEntity.Name!,
                Apellido = OperadorEntity.Apellido!,
                Rol = OperadorEntity.Rol!,
                State = OperadorEntity.State!,
                DepartamentoId = OperadorEntity.DepartamentoId!

            };
        }
    }
}
