using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateBidderCommandHandler : IRequestHandler<UpdateBidderCommand, string>
    {
        private readonly IBidderRepository _bidderRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateBidderCommandHandler(IBidderRepository bidderRepository, IKeycloakService keycloakService)
        {
            _bidderRepository = bidderRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateBidderCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Bidder desde el repositorio
            var opeEntity = await _bidderRepository.GetByIdAsync(request._updateBidderDto.BidderId);
            if (opeEntity == null)
            {
                throw new AuctioneerNotFoundException("Bidder not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(opeEntity.Email))
            {
                throw new ApplicationException("El email del bidder no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateBidderDto.Email))
            {
                opeEntity.Email = request._updateBidderDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.Password))
            {
                opeEntity.Password = request._updateBidderDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.Id))
            {
                opeEntity.Id = request._updateBidderDto.Id;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.Name))
            {
                opeEntity.Name = request._updateBidderDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.LastName))
            {
                opeEntity.LastName = request._updateBidderDto.LastName;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.Phone))
            {
                opeEntity.Phone = request._updateBidderDto.Phone;
            }

            if (!string.IsNullOrEmpty(request._updateBidderDto.Address))
            {
                opeEntity.Address = request._updateBidderDto.Address;
            }

            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = opeEntity.Name,
                lastName = opeEntity.LastName,
                email = opeEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateBidderDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateBidderDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(opeEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _bidderRepository.UpdateAsync(opeEntity);

            return "Bidder Actualizado Correctamente";
        }
    }
}
