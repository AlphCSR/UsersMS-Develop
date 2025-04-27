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
    public class GetAdministradorQueryHandler : IRequestHandler<GetAdministradorQuery, GetAdministradorDto>
    {
        private readonly IAdministradorRepository _administradorRepository;

        public GetAdministradorQueryHandler(IAdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public async Task<GetAdministradorDto> Handle(GetAdministradorQuery request, CancellationToken cancellationToken)
        {
            var administradorEntity = await _administradorRepository.GetByIdAsync(request.AdministradorId);

            if (administradorEntity == null)
                throw new AdministradorNotFoundException("Administrador not found.");

            //mapeando de la entidad al dto - Cliente recibe dto no entidades
            return new GetAdministradorDto
            {
                AdministradorId = administradorEntity.AdministradorId,
                Email = administradorEntity.Email!,
                Password = administradorEntity.Password!,
                Cedula = administradorEntity.Cedula!,
                Name = administradorEntity.Name!,
                Apellido = administradorEntity.Apellido!,
                Rol = administradorEntity.Rol!,
                State = administradorEntity.State!,
                DepartamentoId = administradorEntity.DepartamentoId!,
                EmpresaId = administradorEntity.EmpresaId,

            };
        }
    }
}
