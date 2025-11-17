using AutoMapper;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetAllPowerOfAttorney;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetAllPowerOfAttorney
{
    public class GetAllPowerOfAttorneysHandler : IRequestHandler<GetAllPowerOfAttorneyQuery, IEnumerable<PowerOfAttorneyDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPowerOfAttorneysHandler> _logger;

        public GetAllPowerOfAttorneysHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllPowerOfAttorneysHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PowerOfAttorneyDto>> Handle(GetAllPowerOfAttorneyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب جميع الوكالات...");

            var entities = await _uow.Repository<PowerOfAttorney>()
                .GetFilteredAsync(
                    filter: c => !c.IsDeleted,
                    includeProperties: "Client,Office,Lawyer"
                );

            var result = _mapper.Map<IEnumerable<PowerOfAttorneyDto>>(entities);
            return result;
        }
    }
}
