using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateOperadorCommandHandler : IRequestHandler<UpdateOperadorCommand, string>
    {
        private readonly IOperadorRepository _operadorRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateOperadorCommandHandler(IOperadorRepository operadorRepository, IKeycloakService keycloakService)
        {
            _operadorRepository = operadorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateOperadorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Operador desde el repositorio
            var opeEntity = await _operadorRepository.GetByIdAsync(request._updateOperadorDto.OperadorId);
            if (opeEntity == null)
            {
                throw new OperadorNotFoundException("Operador not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(opeEntity.Email))
            {
                throw new ApplicationException("El email del operador no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateOperadorDto.Email))
            {
                opeEntity.Email = request._updateOperadorDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateOperadorDto.Password))
            {
                opeEntity.Password = request._updateOperadorDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateOperadorDto.Cedula))
            {
                opeEntity.Cedula = request._updateOperadorDto.Cedula;
            }

            if (!string.IsNullOrEmpty(request._updateOperadorDto.Name))
            {
                opeEntity.Name = request._updateOperadorDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateOperadorDto.Apellido))
            {
                opeEntity.Apellido = request._updateOperadorDto.Apellido;
            }

            if (request._updateOperadorDto.Rol.HasValue)
            {
                opeEntity.Rol = request._updateOperadorDto.Rol.Value;
            }

            if (request._updateOperadorDto.State.HasValue)
            {
                opeEntity.State = request._updateOperadorDto.State.Value;
            }

            if (request._updateOperadorDto.DepartamentoId.HasValue)
            {
                opeEntity.DepartamentoId = request._updateOperadorDto.DepartamentoId.Value;
            }

            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = opeEntity.Name,
                lastName = opeEntity.Apellido,
                email = opeEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateOperadorDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateOperadorDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(opeEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _operadorRepository.UpdateAsync(opeEntity);

            return "Operador Actualizado Correctamente";
        }
    }
}
