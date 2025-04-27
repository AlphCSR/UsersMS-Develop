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
    public class GetAllProveedoresQueryHandler : IRequestHandler<GetAllProveedoresQuery, List<GetAllProveedoresDto>>
    {
        private readonly IProveedorRepository _ProveedorRepository;

        public GetAllProveedoresQueryHandler(IProveedorRepository ProveedorRepository)
        {
            _ProveedorRepository = ProveedorRepository;
        }

        public async Task<List<GetAllProveedoresDto>> Handle(GetAllProveedoresQuery request, CancellationToken cancellationToken)
        {
            var Proveedores = await _ProveedorRepository.GetAllAsync();

            if (Proveedores == null)
            {
                throw new ProveedorNotFoundException("Proveedores not found.");

            }
            else
            {
                //como el handler debe retonar una lista de dto correspondiente se deben mapear los datos de las entidades a dto
                var ProveedoresDto = Proveedores.Select(o => new GetAllProveedoresDto
                {
                    ProveedorId = o.ProveedorId,
                    Email = o.Email!,
                    Password = o.Password!,
                    Cedula = o.Cedula!,
                    Name = o.Name!,
                    Apellido = o.Apellido!,
                    Rol = o.Rol!,
                    State = o.State!,
                    DepartamentoId = o.DepartamentoId!,
                }).ToList();

                return ProveedoresDto;


            }
        }
    }
}
