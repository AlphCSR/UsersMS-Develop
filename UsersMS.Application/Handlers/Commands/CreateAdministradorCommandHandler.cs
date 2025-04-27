using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;

namespace UsersMS.Application.Handlers.Commands
{
    public class CreateAdministradorCommandHandler : IRequestHandler<CreateAdministradorCommand, string>
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateAdministradorCommandHandler(IAdministradorRepository administradorRepository, IKeycloakService keycloakService)
        {
            Console.WriteLine("CreateAdministradorCommandHandler instantiated");

            _administradorRepository = administradorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateAdministradorCommand request, CancellationToken cancellationToken)
        {

            // Crear el administrador en la base de datos primero
            var administrador = new Administrador(
                request._createAdministradorDto.Email!,
                request._createAdministradorDto.Password!,
                request._createAdministradorDto.Cedula!,
                request._createAdministradorDto.Name!,
                request._createAdministradorDto.Apellido!,
                request._createAdministradorDto.Rol,
                request._createAdministradorDto.State
            );

            await _administradorRepository.AddAsync(administrador);

            // Obtener el token de administrador de Keycloak
            Console.WriteLine("Fetching admin token from Keycloak");
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el DTO para Keycloak con la información del administrador
            var userDto = new
            {
                username = administrador.Email,
                firstName = administrador.Name,
                lastName = administrador.Apellido,
                email = administrador.Email,
                enabled = true,
                credentials = new[]
                {
                    new { type = "password", value = administrador.Password, temporary = false }
                },
            };

            await _keycloakService.CreateUserAsync(userDto, token);

            // Asignar el rol al usuario en Keycloak
       
            await _keycloakService.AssignRoleAsync(administrador.Email, request._createAdministradorDto.Rol.ToString(), token);

            return "Administrador creado correctamente.";
        }
    }
}
