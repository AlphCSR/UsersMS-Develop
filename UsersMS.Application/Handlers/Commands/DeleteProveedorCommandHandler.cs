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
    public class DeleteProveedorCommandHandler : IRequestHandler<DeleteProveedorCommand, String>
    {
        private readonly IProveedorRepository _ProveedorRepository;

        public DeleteProveedorCommandHandler(IProveedorRepository ProveedorRepository)
        {
            _ProveedorRepository = ProveedorRepository;
        }

        public async Task<String> Handle(DeleteProveedorCommand request, CancellationToken cancellationToken)
        {

            await _ProveedorRepository.DeleteAsync(request._deleteProveedorDto.ProveedorId);
            return "Proveedor eliminado correctamente";
        }
    }
}
