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
    public class GetAllAdministradoresQueryHandler : IRequestHandler<GetAllAdministradoresQuery, List<GetAllAdministradoresDto>>
    {
        private readonly IAdministradorRepository _administradorRepository;

        public GetAllAdministradoresQueryHandler(IAdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public async Task<List<GetAllAdministradoresDto>> Handle(GetAllAdministradoresQuery request, CancellationToken cancellationToken)
        {
            var Administradores = await _administradorRepository.GetAllAsync();

            if (Administradores == null)
            {
                throw new AdministradorNotFoundException("Administradores not found.");

            }
            else
            {
                //como el handler debe retonar una lista de dto correspondiente se deben mapear los datos de las entidades a dto
                var AdministradoresDto = Administradores.Select(o => new GetAllAdministradoresDto
                {
                    AdministradorId = o.AdministradorId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Cedula = o.Cedula!,
                    Name = o.Name!,
                    Apellido = o.Apellido!,
                    Rol = o.Rol!,
                    State = o.State!,
                    DepartamentoId = o.DepartamentoId!,
                    EmpresaId = o.EmpresaId,
                }).ToList();

                return AdministradoresDto;


            }
        }
    }
}
