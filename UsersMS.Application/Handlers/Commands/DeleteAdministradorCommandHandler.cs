using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;

namespace UsersMS.Application.Handlers.Commands
{
    public class DeleteAdministradorCommandHandler : IRequestHandler<DeleteAdministradorCommand, string>
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IKeycloakService _keycloakService;

        public DeleteAdministradorCommandHandler(IAdministradorRepository administradorRepository, IKeycloakService keycloakService)
        {
            _administradorRepository = administradorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(DeleteAdministradorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el Administrador desde el repositorio
            var administrador = await _administradorRepository.GetByIdAsync(request._deleteAdministradorDto.AdministradorId);
            if (administrador == null)
            {
                throw new ApplicationException($"No se encontró un administrador con el ID {request._deleteAdministradorDto.AdministradorId}");
            }

            // Validar el email antes de proceder
            if (string.IsNullOrEmpty(administrador.Email))
            {
                throw new ApplicationException("El email del administrador no puede ser nulo o vacío.");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Deshabilitar el usuario en Keycloak
            try
            {
                await _keycloakService.DisableUserAsync(administrador.Email, adminToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al deshabilitar el usuario en Keycloak: {ex.Message}");
            }

            // Eliminar el administrador en la base de datos
            await _administradorRepository.DeleteAsync(request._deleteAdministradorDto.AdministradorId);

            return "Administrador deshabilitado correctamente";
        }

    }
}
