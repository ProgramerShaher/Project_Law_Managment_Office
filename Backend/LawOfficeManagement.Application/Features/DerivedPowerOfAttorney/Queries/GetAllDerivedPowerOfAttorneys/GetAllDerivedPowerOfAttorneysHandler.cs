using AutoMapper;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetAllDerivedPowerOfAttorneys
{
    public class GetAllDerivedPowerOfAttorneysHandler : IRequestHandler<GetAllDerivedPowerOfAttorneysQuery, IEnumerable<DerivedPowerOfAttorneyDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllDerivedPowerOfAttorneysHandler> _logger;

        public GetAllDerivedPowerOfAttorneysHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllDerivedPowerOfAttorneysHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DerivedPowerOfAttorneyDto>> Handle(GetAllDerivedPowerOfAttorneysQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب جميع الوكالات المشتقة...");

            var entities = await _uow.Repository<DerivedPowerOfAttorney>()
                .GetFilteredAsync(
                    filter: c => !c.IsDeleted,
                    includeProperties: "ParentPowerOfAttorney,Lawyer"
                );

            var result = _mapper.Map<IEnumerable<DerivedPowerOfAttorneyDto>>(entities);
            return result;
        }
    }
}