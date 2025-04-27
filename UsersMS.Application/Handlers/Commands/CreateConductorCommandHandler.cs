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
    public class CreateConductorCommandHandler : IRequestHandler<CreateConductorCommand, string>
    {
        private readonly IConductorRepository _conductorRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateConductorCommandHandler(IConductorRepository conductorRepository, IKeycloakService keycloakService)
        {
            _conductorRepository = conductorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateConductorCommand request, CancellationToken cancellationToken)
        {
            // Validar el DTO
           // var validator = new CreateConductorValidator();
           // await validator.ValidateRequest(request._createConductorDto);

            // Crear la entidad Conductor
            var conductor = new Conductor(
                request._createConductorDto.Email!,
                request._createConductorDto.Password!,
                request._createConductorDto.Cedula!,
                request._createConductorDto.Name!,
                request._createConductorDto.Apellido!,
                request._createConductorDto.Rol,
                request._createConductorDto.State,
                request._createConductorDto.CertificadoSalud!,
                request._createConductorDto.Licencia!
            );

            // Guardar el conductor en la base de datos
            await _conductorRepository.AddAsync(conductor);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = conductor.Email,
                    email = conductor.Email,
                    firstName = conductor.Name,
                    lastName = conductor.Apellido,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = conductor.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(conductor.Email, conductor.Rol.ToString(), token);

            return "Conductor Creado Correctamente";
        }
    }
}
