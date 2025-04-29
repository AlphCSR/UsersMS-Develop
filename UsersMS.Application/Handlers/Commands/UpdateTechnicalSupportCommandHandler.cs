using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateTechnicalSupportCommandHandler : IRequestHandler<UpdateTechnicalSupportCommand, string>
    {
        private readonly ITechnicalSupportRepository _technicalSupportRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateTechnicalSupportCommandHandler(ITechnicalSupportRepository technicalSupportRepository, IKeycloakService keycloakService)
        {
            _technicalSupportRepository = technicalSupportRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateTechnicalSupportCommand request, CancellationToken cancellationToken)
        {
            // Obtener el TechnicalSupport desde el repositorio
            var opeEntity = await _technicalSupportRepository.GetByIdAsync(request._updateTechnicalSupportDto.TechnicalSupportId);
            if (opeEntity == null)
            {
                throw new TechnicalSupportNotFoundException("TechnicalSupport not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(opeEntity.Email))
            {
                throw new ApplicationException("El email del technicalSupport no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Email))
            {
                opeEntity.Email = request._updateTechnicalSupportDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Password))
            {
                opeEntity.Password = request._updateTechnicalSupportDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Id))
            {
                opeEntity.Id = request._updateTechnicalSupportDto.Id;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Name))
            {
                opeEntity.Name = request._updateTechnicalSupportDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.LastName))
            {
                opeEntity.LastName = request._updateTechnicalSupportDto.LastName;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Phone))
            {
                opeEntity.Phone = request._updateTechnicalSupportDto.Phone;
            }

            if (!string.IsNullOrEmpty(request._updateTechnicalSupportDto.Address))
            {
                opeEntity.Address = request._updateTechnicalSupportDto.Address;
            }


            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = opeEntity.Name,
                lastName = opeEntity.LastName,
                email = opeEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateTechnicalSupportDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateTechnicalSupportDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(opeEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _technicalSupportRepository.UpdateAsync(opeEntity);

            return "TechnicalSupport Actualizado Correctamente";
        }
    }
}
