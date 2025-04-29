using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Response;
using UsersMS.Core.Repositories;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Querys
{
    public class GetAdministratorQueryHandler : IRequestHandler<GetAdministratorQuery, GetAdministratorDto>
    {
        private readonly IAdministratorRepository _administradorRepository;

        public GetAdministratorQueryHandler(IAdministratorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public async Task<GetAdministratorDto> Handle(GetAdministratorQuery request, CancellationToken cancellationToken)
        {
            var administradorEntity = await _administradorRepository.GetByIdAsync(request.AdministradorId);

            if (administradorEntity == null)
                throw new AdministratorNotFoundException("Administrator not found.");

            return new GetAdministratorDto
            {
                AdministratorId = administradorEntity.AdministratorId,
                Email = administradorEntity.Email!,
                Password = administradorEntity.Password!,
                Id = administradorEntity.Id!,
                Name = administradorEntity.Name!,
                LastName = administradorEntity.LastName!,
                Phone = administradorEntity.Phone!,
                Address = administradorEntity.Address!,
                Role = administradorEntity.Role!,
                State = administradorEntity.State!,
            };
        }
    }
}
