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
    public class DeleteConductorCommandHandler : IRequestHandler<DeleteConductorCommand, String>
    {
        private readonly IConductorRepository _ConductorRepository;

        public DeleteConductorCommandHandler(IConductorRepository ConductorRepository)
        {
            _ConductorRepository = ConductorRepository;
        }

        public async Task<String> Handle(DeleteConductorCommand request, CancellationToken cancellationToken)
        {

            await _ConductorRepository.DeleteAsync(request._deleteConductorDto.ConductorId);
            return "Conductor eliminado correctamente";
        }
    }
}
