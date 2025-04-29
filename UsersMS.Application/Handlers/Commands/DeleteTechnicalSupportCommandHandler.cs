using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Commands;
using UsersMS.Core.Repositories;

namespace UsersMS.Application.Handlers.Commands
{
    public class DeleteTechnicalSupportCommandHandler : IRequestHandler<DeleteTechnicalSupportCommand, String>
    {
        private readonly ITechnicalSupportRepository _TechnicalSupportRepository;

        public DeleteTechnicalSupportCommandHandler(ITechnicalSupportRepository ProveedorRepository)
        {
            _TechnicalSupportRepository = ProveedorRepository;
        }

        public async Task<String> Handle(DeleteTechnicalSupportCommand request, CancellationToken cancellationToken)
        {

            await _TechnicalSupportRepository.DeleteAsync(request._deleteTechnicalSupportDto.TechnicalSupportId);
            return "Proveedor eliminado correctamente";
        }
    }
}
