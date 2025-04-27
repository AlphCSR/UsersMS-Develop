using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateProveedorCommandHandler : IRequestHandler<UpdateProveedorCommand, string>
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateProveedorCommandHandler(IProveedorRepository proveedorRepository, IKeycloakService keycloakService)
        {
            _proveedorRepository = proveedorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateProveedorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Proveedor desde el repositorio
            var opeEntity = await _proveedorRepository.GetByIdAsync(request._updateProveedorDto.ProveedorId);
            if (opeEntity == null)
            {
                throw new ProveedorNotFoundException("Proveedor not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(opeEntity.Email))
            {
                throw new ApplicationException("El email del proveedor no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateProveedorDto.Email))
            {
                opeEntity.Email = request._updateProveedorDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateProveedorDto.Password))
            {
                opeEntity.Password = request._updateProveedorDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateProveedorDto.Cedula))
            {
                opeEntity.Cedula = request._updateProveedorDto.Cedula;
            }

            if (!string.IsNullOrEmpty(request._updateProveedorDto.Name))
            {
                opeEntity.Name = request._updateProveedorDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateProveedorDto.Apellido))
            {
                opeEntity.Apellido = request._updateProveedorDto.Apellido;
            }

            if (request._updateProveedorDto.Rol.HasValue)
            {
                opeEntity.Rol = request._updateProveedorDto.Rol.Value;
            }

            if (request._updateProveedorDto.State.HasValue)
            {
                opeEntity.State = request._updateProveedorDto.State.Value;
            }

            if (request._updateProveedorDto.DepartamentoId.HasValue)
            {
                opeEntity.DepartamentoId = request._updateProveedorDto.DepartamentoId.Value;
            }

            if (request._updateProveedorDto.EmpresaId.HasValue)
            {
                opeEntity.EmpresaId = request._updateProveedorDto.EmpresaId.Value;
            }

            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = opeEntity.Name,
                lastName = opeEntity.Apellido,
                email = opeEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateProveedorDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateProveedorDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(opeEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _proveedorRepository.UpdateAsync(opeEntity);

            return "Proveedor Actualizado Correctamente";
        }
    }
}
