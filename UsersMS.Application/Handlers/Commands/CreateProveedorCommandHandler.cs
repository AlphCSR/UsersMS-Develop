using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;

namespace UsersMS.Application.Handlers.Commands
{
    public class CreateProveedorCommandHandler : IRequestHandler<CreateProveedorCommand, string>
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IKeycloakService _keycloakService;

        public CreateProveedorCommandHandler(IProveedorRepository proveedorRepository, IKeycloakService keycloakService)
        {
            _proveedorRepository = proveedorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(CreateProveedorCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad Proveedor
            var proveedor = new Proveedor(
                request._createProveedorDto.Email!,
                request._createProveedorDto.Password!,
                request._createProveedorDto.Cedula!,
                request._createProveedorDto.Name!,
                request._createProveedorDto.Apellido!,
                request._createProveedorDto.Rol,
                request._createProveedorDto.State
            );

            // Guardar el proveedor en la base de datos
            await _proveedorRepository.AddAsync(proveedor);

            // Obtener el token de administrador de Keycloak
            var token = await _keycloakService.GetAdminTokenAsync();

            // Crear el usuario en Keycloak
            await _keycloakService.CreateUserAsync(
                new
                {
                    username = proveedor.Email,
                    email = proveedor.Email,
                    firstName = proveedor.Name,
                    lastName = proveedor.Apellido,
                    enabled = true,
                    credentials = new[]
                    {
                        new { type = "password", value = proveedor.Password, temporary = false }
                    }
                },
                token
            );

            // Asignar el rol en Keycloak
            await _keycloakService.AssignRoleAsync(proveedor.Email, proveedor.Rol.ToString(), token);

            return "Proveedor Creado Correctamente";
        }
    }
}
