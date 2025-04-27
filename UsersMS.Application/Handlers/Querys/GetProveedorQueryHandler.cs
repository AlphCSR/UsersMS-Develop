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
    public class GetProveedorQueryHandler : IRequestHandler<GetProveedorQuery, GetProveedorDto>
    {
        private readonly IProveedorRepository _ProveedorRepository;

        public GetProveedorQueryHandler(IProveedorRepository ProveedorRepository)
        {
            _ProveedorRepository = ProveedorRepository;
        }

        public async Task<GetProveedorDto> Handle(GetProveedorQuery request, CancellationToken cancellationToken)
        {
            var ProveedorEntity = await _ProveedorRepository.GetByIdAsync(request.ProveedorId);

            if (ProveedorEntity == null)
                throw new ProveedorNotFoundException("Proveedor not found.");

            //mapeando de la entidad al dto - Cliente recibe dto no entidades
            return new GetProveedorDto
            {
                ProveedorId = ProveedorEntity.ProveedorId,
                Email = ProveedorEntity.Email!,
                Password = ProveedorEntity.Password!,
                Cedula = ProveedorEntity.Cedula!,
                Name = ProveedorEntity.Name!,
                Apellido = ProveedorEntity.Apellido!,
                Rol = ProveedorEntity.Rol!,
                State = ProveedorEntity.State!,
                DepartamentoId = ProveedorEntity.DepartamentoId!,
                EmpresaId = ProveedorEntity.EmpresaId!,

            };
        }
    }
}
