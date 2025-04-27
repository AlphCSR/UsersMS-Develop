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
    public class CreateOperadorCommandHandler : IRequestHandler<CreateOperadorCommand, string>
    {
        private readonly IOperadorRepository _operadorRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateOperadorCommandHandler(IOperadorRepository operadorRepository, IKeycloakService keycloakService)
        {
            _operadorRepository = operadorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateOperadorCommand request, CancellationToken cancellationToken)
        {
            // Validar el DTO
          //  var validator = new CreateOperadorValidator();
          //  await validator.ValidateRequest(request._createOperadorDto);

            // Crear la entidad Operador
            var operador = new Operador(
                request._createOperadorDto.Email!,
                request._createOperadorDto.Password!,
                request._createOperadorDto.Cedula!,
                request._createOperadorDto.Name!,
                request._createOperadorDto.Apellido!,
                request._createOperadorDto.Rol,
                request._createOperadorDto.State
            );

            // Guardar el operador en la base de datos
            await _operadorRepository.AddAsync(operador);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = operador.Email,
                    email = operador.Email,
                    firstName = operador.Name,
                    lastName = operador.Apellido,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = operador.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(operador.Email, operador.Rol.ToString(), token);

            return "Operador Creado Correctamente";
        }
    }
}
