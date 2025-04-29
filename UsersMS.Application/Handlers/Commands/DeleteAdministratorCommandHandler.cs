using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;

namespace UsersMS.Application.Handlers.Commands
{
    public class DeleteAdministratorCommandHandler : IRequestHandler<DeleteAdministratorCommand, string>
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IKeycloakService _keycloakService;

        public DeleteAdministratorCommandHandler(IAdministratorRepository administratorRepository, IKeycloakService keycloakService)
        {
            _administratorRepository = administratorRepository;
            _keycloakService = keycloakService;
        }

        public async Task<string> Handle(DeleteAdministratorCommand request, CancellationToken cancellationToken)
        {
            // Obtener el administrator desde el repositorio
            var administrator = await _administratorRepository.GetByIdAsync(request._deleteAdministratorDto.AdministratorId);
            if (administrator == null)
            {
                throw new ApplicationException($"No administrator found with the ID {request._deleteAdministratorDto.AdministratorId}");
            }

            // Validar el email antes de proceder
            if (string.IsNullOrEmpty(administrator.Email))
            {
                throw new ApplicationException("The administrator email cannot be null or empty..");
            }

            // Obtener el token de Keycloak
            var adminToken = await _keycloakService.GetAdminTokenAsync();

            // Deshabilitar el usuario en Keycloak
            try
            {
                await _keycloakService.DisableUserAsync(administrator.Email, adminToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error disabling user in Keycloak: {ex.Message}");
            }

            // Eliminar el administrator en la base de datos
            await _administratorRepository.DeleteAsync(request._deleteAdministratorDto.AdministratorId);

            return "Aadministrator successfully disabled";
        }

    }
}
