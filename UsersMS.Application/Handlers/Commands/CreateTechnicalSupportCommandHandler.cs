using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;

namespace UsersMS.Application.Handlers.Commands
{
    public class CreateTechnicalSupportCommandHandler : IRequestHandler<CreateTechnicalSupportCommand, string>
    {
        private readonly ITechnicalSupportRepository _technicalSupportRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateTechnicalSupportCommandHandler(ITechnicalSupportRepository technicalSupportRepository, IKeycloakService keycloakService)
        {
            _technicalSupportRepository = technicalSupportRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateTechnicalSupportCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad technicalSupport
            var technicalSupport = new TechnicalSupport(
                request._createTechnicalSupportDto.Email!,
                request._createTechnicalSupportDto.Password!,
                request._createTechnicalSupportDto.Id!,
                request._createTechnicalSupportDto.Name!,
                request._createTechnicalSupportDto.LastName!,
                request._createTechnicalSupportDto.Phone!,
                request._createTechnicalSupportDto.Address!,
                request._createTechnicalSupportDto.Role,
                request._createTechnicalSupportDto.State
            );

            // Guardar el technicalSupport en la base de datos
            await _technicalSupportRepository.AddAsync(technicalSupport);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = technicalSupport.Email,
                    email = technicalSupport.Email,
                    firstName = technicalSupport.Name,
                    lastName = technicalSupport.LastName,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = technicalSupport.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(technicalSupport.Email, technicalSupport.Role.ToString(), token);

            return "Technical Support Creado Correctamente";
        }
    }
}
