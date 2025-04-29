using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Commands
{
    public class UpdateAuctioneerCommandHandler : IRequestHandler<UpdateAuctioneerCommand, string>
    {
        private readonly IAuctioneerRepository _auctioneerRepository;
        private readonly IKeycloakService _keycloakService;

        public UpdateAuctioneerCommandHandler(IAuctioneerRepository auctioneerRepository, IKeycloakService keycloakService)
        {
            _auctioneerRepository = auctioneerRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(UpdateAuctioneerCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Auctioneer desde el repositorio
            var condEntity = await _auctioneerRepository.GetByIdAsync(request._updateAuctioneerDto.AuctioneerId);
            if (condEntity == null)
            {
                throw new BidderNotFoundException("Auctioneer not found.");
            }

            // Validar el email antes de continuar
            if (string.IsNullOrEmpty(condEntity.Email))
            {
                throw new ApplicationException("El email del auctioneer no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Actualizar las propiedades de la entidad si hay cambios en el DTO
            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Email))
            {
                condEntity.Email = request._updateAuctioneerDto.Email;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Password))
            {
                condEntity.Password = request._updateAuctioneerDto.Password;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Id))
            {
                condEntity.Id = request._updateAuctioneerDto.Id;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Name))
            {
                condEntity.Name = request._updateAuctioneerDto.Name;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.LastName))
            {
                condEntity.LastName = request._updateAuctioneerDto.LastName;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Phone))
            {
                condEntity.Phone = request._updateAuctioneerDto.Phone;
            }

            if (!string.IsNullOrEmpty(request._updateAuctioneerDto.Address))
            {
                condEntity.Address = request._updateAuctioneerDto.Address;
            }


            // Crear el payload para actualizar en Keycloak
            var updatePayload = new
            {
                firstName = condEntity.Name,
                lastName = condEntity.LastName,
                email = condEntity.Email,
                credentials = !string.IsNullOrEmpty(request._updateAuctioneerDto.Password)
                    ? new[]
                    {
                        new { type = "password", value = request._updateAuctioneerDto.Password, temporary = false }
                    }
                    : null
            };

            // Actualizar el usuario en Keycloak utilizando el email como username
            await _keycloakService.UpdateUserAsync(condEntity.Email, updatePayload, adminToken);

            // Actualizar en la base de datos
            await _auctioneerRepository.UpdateAsync(condEntity);

            return "Auctioneer Actualizado Correctamente";
        }
    }
}
