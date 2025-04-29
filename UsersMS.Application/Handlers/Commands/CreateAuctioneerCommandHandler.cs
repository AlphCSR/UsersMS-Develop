using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Application.Validator;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;

namespace UsersMS.Application.Handlers.Commands
{
    public class CreateAuctioneerCommandHandler : IRequestHandler<CreateAuctioneerCommand, string>
    {
        private readonly IAuctioneerRepository _auctioneerRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateAuctioneerCommandHandler(IAuctioneerRepository auctioneerRepository, IKeycloakService keycloakService)
        {
            _auctioneerRepository = auctioneerRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateAuctioneerCommand request, CancellationToken cancellationToken)
        {

            var auctioneer = new Auctioneer(
                request._createAuctioneerDto.Email!,
                request._createAuctioneerDto.Password!,
                request._createAuctioneerDto.Id!,
                request._createAuctioneerDto.Name!,
                request._createAuctioneerDto.LastName!,
                request._createAuctioneerDto.Phone!,
                request._createAuctioneerDto.Address!,
                request._createAuctioneerDto.Role,
                request._createAuctioneerDto.State
            );

            // Guardar el auctioneer en la base de datos
            await _auctioneerRepository.AddAsync(auctioneer);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = auctioneer.Email,
                    email = auctioneer.Email,
                    firstName = auctioneer.Name,
                    lastName = auctioneer.LastName,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = auctioneer.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(auctioneer.Email, auctioneer.Role.ToString(), token);

            return "Auctioneer Creado Correctamente";
        }
    }
}
