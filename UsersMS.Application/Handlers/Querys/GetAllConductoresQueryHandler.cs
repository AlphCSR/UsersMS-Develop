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
    public class GetAllConductoresQueryHandler : IRequestHandler<GetAllConductoresQuery, List<GetAllConductoresDto>>
    {
        private readonly IConductorRepository _ConductorRepository;

        public GetAllConductoresQueryHandler(IConductorRepository ConductorRepository)
        {
            _ConductorRepository = ConductorRepository;
        }

        public async Task<List<GetAllConductoresDto>> Handle(GetAllConductoresQuery request, CancellationToken cancellationToken)
        {
            var Conductores = await _ConductorRepository.GetAllAsync();

            if (Conductores == null)
            {
                throw new ConductorNotFoundException("Conductores not found.");

            }
            else
            {
                //como el handler debe retonar una lista de dto correspondiente se deben mapear los datos de las entidades a dto
                var ConductoresDto = Conductores.Select(o => new GetAllConductoresDto
                {
                    ConductorId = o.ConductorId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Cedula = o.Cedula!,
                    Name = o.Name!,
                    Apellido = o.Apellido!,
                    Licencia = o.Licencia!,
                    CertificadoSalud = o.CertificadoSalud!,
                    Rol = o.Rol!,
                    State = o.State!,
                    DepartamentoId = o.DepartamentoId!,
                    EmpresaId = o.EmpresaId!,
                    GruaId = o.GruaId!,
                }).ToList();

                return ConductoresDto;


            }
        }
    }
}
