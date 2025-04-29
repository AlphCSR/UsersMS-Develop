using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Application.Querys;
using UsersMS.Commons.Dtos.Response;
using UsersMS.Core.Repositories;
using UsersMS.Domain.Entities;
using UsersMS.Infrastructure.Exceptions;

namespace UsersMS.Application.Handlers.Querys
{
    public class GetTechnicalSupportQueryHandler : IRequestHandler<GetTechnicalSupportQuery, GetTechnicalSupportDto>
    {
        private readonly ITechnicalSupportRepository _TechnicalSupportRepository;

        public GetTechnicalSupportQueryHandler(ITechnicalSupportRepository TechnicalSupportRepository)
        {
            _TechnicalSupportRepository = TechnicalSupportRepository;
        }

        public async Task<GetTechnicalSupportDto> Handle(GetTechnicalSupportQuery request, CancellationToken cancellationToken)
        {
            var TechnicalSupportEntity = await _TechnicalSupportRepository.GetByIdAsync(request.TechnicalSupportId);

            if (TechnicalSupportEntity == null)
                throw new TechnicalSupportNotFoundException("TechnicalSupport not found.");

            return new GetTechnicalSupportDto
            {
                TechnicalSupportId = TechnicalSupportEntity.TechnicalSupportId,
                Email = TechnicalSupportEntity.Email!,
                Password = TechnicalSupportEntity.Password!,
                Id = TechnicalSupportEntity.Id!,
                Name = TechnicalSupportEntity.Name!,
                LastName = TechnicalSupportEntity.LastName!,
                Role = TechnicalSupportEntity.Role!,
                Phone = TechnicalSupportEntity.Phone!,
                Address = TechnicalSupportEntity.Address!,
                State = TechnicalSupportEntity.State!,
            };
        }
    }
}
