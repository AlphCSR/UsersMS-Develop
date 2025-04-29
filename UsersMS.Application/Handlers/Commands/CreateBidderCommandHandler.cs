using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Application.Validator;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;
using UsersMS.Infrastructure.Repositories;

namespace UsersMS.Application.Handlers.Commands
{
    public class CreateBidderCommandHandler : IRequestHandler<CreateBidderCommand, string>
    {
        private readonly IBidderRepository _bidderRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateBidderCommandHandler(IBidderRepository bidderRepository, IKeycloakService keycloakService)
        {
            _bidderRepository = bidderRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateBidderCommand request, CancellationToken cancellationToken)
        {

            var bidder = new Bidder(
                request._createBidderDto.Email!,
                request._createBidderDto.Password!,
                request._createBidderDto.Id!,
                request._createBidderDto.Name!,
                request._createBidderDto.LastName!,
                request._createBidderDto.Phone!,
                request._createBidderDto.Address!,
                request._createBidderDto.Role,
                request._createBidderDto.State
            );

            // Guardar el bidder en la base de datos
            await _bidderRepository.AddAsync(bidder);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = bidder.Email,
                    email = bidder.Email,
                    firstName = bidder.Name,
                    lastName = bidder.LastName,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = bidder.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(bidder.Email, bidder.Role.ToString(), token);

            return "Bidder Creado Correctamente";
        }
    }
}
