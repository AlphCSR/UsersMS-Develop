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
    public class GetAllAdministratorsQueryHandler : IRequestHandler<GetAllAdministratorsQuery, List<GetAllAdministratorsDto>>
    {
        private readonly IAdministratorRepository _administradorRepository;

        public GetAllAdministratorsQueryHandler(IAdministratorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public async Task<List<GetAllAdministratorsDto>> Handle(GetAllAdministratorsQuery request, CancellationToken cancellationToken)
        {
            var Administradores = await _administradorRepository.GetAllAsync();

            if (Administradores == null)
            {
                throw new AdministratorNotFoundException("Administrators not found.");

            }
            else
            {
                var AdministradoresDto = Administradores.Select(o => new GetAllAdministratorsDto
                {
                    AdministratorId = o.AdministratorId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Id = o.Id!,
                    Name = o.Name!,
                    LastName = o.LastName!,
                    Role = o.Role!,
                    Phone = o.Phone!,
                    Address = o.Address!,
                    State = o.State!,
                }).ToList();

                return AdministradoresDto;
            }
        }
    }
}
