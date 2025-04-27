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
    public class DeleteOperadorCommandHandler : IRequestHandler<DeleteOperadorCommand, String>
    {
        private readonly IOperadorRepository _OperadorRepository;
         
        public DeleteOperadorCommandHandler(IOperadorRepository OperadorRepository)
        {
            _OperadorRepository = OperadorRepository;
        }

        public async Task<String> Handle(DeleteOperadorCommand request, CancellationToken cancellationToken)
        {

            await _OperadorRepository.DeleteAsync(request._deleteOperadorDto.OperadorId);
            return "Operador eliminado correctamente";
        }
    }
}
