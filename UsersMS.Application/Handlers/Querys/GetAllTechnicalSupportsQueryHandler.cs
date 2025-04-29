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
    public class GetAllTechnicalSupportsQueryHandler : IRequestHandler<GetAllTechnicalSupportsQuery, List<GetAllTechnicalSupportsDto>>
    {
        private readonly ITechnicalSupportRepository _TechnicalSupportRepository;

        public GetAllTechnicalSupportsQueryHandler(ITechnicalSupportRepository TechnicalSupportRepository)
        {
            _TechnicalSupportRepository = TechnicalSupportRepository;
        }

        public async Task<List<GetAllTechnicalSupportsDto>> Handle(GetAllTechnicalSupportsQuery request, CancellationToken cancellationToken)
        {
            var TechnicalSupport = await _TechnicalSupportRepository.GetAllAsync();

            if (TechnicalSupport == null)
            {
                throw new TechnicalSupportNotFoundException("TechnicalSupports not found.");
            }
            else
            {
                var TechnicalSupportsDto = TechnicalSupport.Select(o => new GetAllTechnicalSupportsDto
                {
                    TechnicalSupportId = o.TechnicalSupportId,
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

                return TechnicalSupportsDto;
            }
        }
    }
}
