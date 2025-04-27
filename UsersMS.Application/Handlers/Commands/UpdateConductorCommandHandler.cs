using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateConductorCommandHandler : IRequestHandler<UpdateConductorCommand, string>
    {
        private readonly IConductorRepository _conductorRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateConductorCommandHandler(IConductorRepository conductorRepository, IKeycloakService keycloakService)
        {
            _conductorRepository = conductorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateConductorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Conductor desde el repositorio
            var condEntity = await _conductorRepository.GetByIdAsync(request._updateConductorDto.ConductorId);
            if (condEntity == null)
            {
                throw new ConductorNotFoundException("Conductor not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(condEntity.Email))
            {
                throw new ApplicationException("El email del conductor no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateConductorDto.Email))
            {
                condEntity.Email = request._updateConductorDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateConductorDto.Password))
            {
                condEntity.Password = request._updateConductorDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateConductorDto.Cedula))
            {
                condEntity.Cedula = request._updateConductorDto.Cedula;
            }

            if (!string.IsNullOrEmpty(request._updateConductorDto.Name))
            {
                condEntity.Name = request._updateConductorDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateConductorDto.Apellido))
            {
                condEntity.Apellido = request._updateConductorDto.Apellido;
            }

            if (request._updateConductorDto.Licencia.HasValue)
            {
                condEntity.Licencia = request._updateConductorDto.Licencia.Value;
            }

            if (request._updateConductorDto.CertificadoSalud.HasValue)
            {
                condEntity.CertificadoSalud = request._updateConductorDto.CertificadoSalud.Value;
            }

            if (request._updateConductorDto.Rol.HasValue)
            {
                condEntity.Rol = request._updateConductorDto.Rol.Value;
            }

            if (request._updateConductorDto.State.HasValue)
            {
                condEntity.State = request._updateConductorDto.State.Value;
            }

            if (request._updateConductorDto.DepartamentoId.HasValue)
            {
                condEntity.DepartamentoId = request._updateConductorDto.DepartamentoId.Value;
            }

            if (request._updateConductorDto.EmpresaId.HasValue)
            {
                condEntity.EmpresaId = request._updateConductorDto.EmpresaId.Value;
            }

            if (request._updateConductorDto.GruaId.HasValue)
            {
                condEntity.GruaId = request._updateConductorDto.GruaId.Value;
            }

            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = condEntity.Name,
                lastName = condEntity.Apellido,
                email = condEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateConductorDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateConductorDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(condEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _conductorRepository.UpdateAsync(condEntity);

            return "Conductor Actualizado Correctamente";
        }
    }
}
