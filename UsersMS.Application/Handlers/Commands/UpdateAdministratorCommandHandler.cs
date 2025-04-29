using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateAdministratorCommandHandler : IRequestHandler<UpdateAdministratorCommand, string>
    {
        private readonly IAdministratorRepository _administradorRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateAdministratorCommandHandler(IAdministratorRepository administradorRepository, IKeycloakService keycloakService)
        {
            _administradorRepository = administradorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateAdministratorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Administrador desde el repositorio
            var adminEntity = await _administradorRepository.GetByIdAsync(request._updateAdministratorDto.AdministratorId);
            if (adminEntity == null)
            {
                throw new AdministratorNotFoundException("Administrador no encontrado.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(adminEntity.Email))
            {
                throw new ApplicationException("El email del administrador no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Email))
            {
                adminEntity.Email = request._updateAdministratorDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Password))
            {
                adminEntity.Password = request._updateAdministratorDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Id))
            {
                adminEntity.Id = request._updateAdministratorDto.Id;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Name))
            {
                adminEntity.Name = request._updateAdministratorDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.LastName))
            {
                adminEntity.LastName = request._updateAdministratorDto.LastName;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Phone))
            {
                adminEntity.Phone = request._updateAdministratorDto.Phone;
            }

            if (!string.IsNullOrEmpty(request._updateAdministratorDto.Address))
            {
                adminEntity.Address = request._updateAdministratorDto.Address;
            }

            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = adminEntity.Name,
                lastName = adminEntity.LastName,
                email = adminEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateAdministratorDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateAdministratorDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(adminEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _administradorRepository.UpdateAsync(adminEntity);
            return "Administrador Actualizado Correctamente";
        }
    }
}
