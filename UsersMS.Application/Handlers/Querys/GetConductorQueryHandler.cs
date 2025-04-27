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
    public class GetConductorQueryHandler : IRequestHandler<GetConductorQuery, GetConductorDto>
    {
        private readonly IConductorRepository _ConductorRepository;

        public GetConductorQueryHandler(IConductorRepository ConductorRepository)
        {
            _ConductorRepository = ConductorRepository;
        }

        public async Task<GetConductorDto> Handle(GetConductorQuery request, CancellationToken cancellationToken)
        {
            var ConductorEntity = await _ConductorRepository.GetByIdAsync(request.ConductorId);

            if (ConductorEntity == null)
                throw new ConductorNotFoundException("Conductor not found.");

            //mapeando de la entidad al dto - Cliente recibe dto no entidades
            return new GetConductorDto
            {
                ConductorId = ConductorEntity.ConductorId,
                Email = ConductorEntity.Email!,
                Password = ConductorEntity.Password!,
                Cedula = ConductorEntity.Cedula!,
                Name = ConductorEntity.Name!,
                Apellido = ConductorEntity.Apellido!,
                Rol = ConductorEntity.Rol!,
                State = ConductorEntity.State!,
                Licencia = ConductorEntity.Licencia!,
                CertificadoSalud=ConductorEntity.CertificadoSalud!,
                DepartamentoId = ConductorEntity.DepartamentoId!,
                EmpresaId = ConductorEntity.EmpresaId,
                GruaId = ConductorEntity.GruaId

            };
        }
    }
}
