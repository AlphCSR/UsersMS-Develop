using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateAdministradorCommandHandler : IRequestHandler<UpdateAdministradorCommand, string>
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateAdministradorCommandHandler(IAdministradorRepository administradorRepository, IKeycloakService keycloakService)
        {
            _administradorRepository = administradorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateAdministradorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Administrador desde el repositorio
            var adminEntity = await _administradorRepository.GetByIdAsync(request._updateAdministradorDto.AdministradorId);
            if (adminEntity == null)
            {
                throw new AdministradorNotFoundException("Administrador no encontrado.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(adminEntity.Email))
            {
                throw new ApplicationException("El email del administrador no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();
            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateAdministradorDto.Email))
            {
                adminEntity.Email = request._updateAdministradorDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateAdministradorDto.Password))
            {
                adminEntity.Password = request._updateAdministradorDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateAdministradorDto.Cedula))
            {
                adminEntity.Cedula = request._updateAdministradorDto.Cedula;
            }

            if (!string.IsNullOrEmpty(request._updateAdministradorDto.Name))
            {
                adminEntity.Name = request._updateAdministradorDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateAdministradorDto.Apellido))
            {
                adminEntity.Apellido = request._updateAdministradorDto.Apellido;
            }

            // Si el rol cambia, lo actualizamos en Keycloak mediante AssignRoleAsync
            /*if (request._updateAdministradorDto.Rol.HasValue && request._updateAdministradorDto.Rol.Value != adminEntity.Rol)
            {
                Console.WriteLine($"Token 3routilizado: {adminToken.Substring(0, 20)}...");
                var newRole = request._updateAdministradorDto.Rol.Value.ToString();
                await _keycloakService.AssignRoleAsync(adminEntity.Email, newRole, adminToken);

                // Actualizar el rol en la entidad
                adminEntity.Rol = request._updateAdministradorDto.Rol.Value;
            }*/
            if (request._updateAdministradorDto.State.HasValue)
            {
                adminEntity.State = request._updateAdministradorDto.State.Value;
            }

            if (request._updateAdministradorDto.EmpresaId != null)
            {
                adminEntity.EmpresaId = request._updateAdministradorDto.EmpresaId;
            }

            if (request._updateAdministradorDto.DepartamentoId.HasValue)
            {
                adminEntity.DepartamentoId = request._updateAdministradorDto.DepartamentoId.Value;
            }
            
            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = adminEntity.Name,
                lastName = adminEntity.Apellido,
                email = adminEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateAdministradorDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateAdministradorDto.Password, temporary = false }
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
