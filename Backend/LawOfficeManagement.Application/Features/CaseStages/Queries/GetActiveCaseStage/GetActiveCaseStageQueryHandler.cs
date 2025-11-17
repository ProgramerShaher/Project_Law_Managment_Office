using AutoMapper;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetActiveCaseStage
{

    public class GetActiveCaseStageQuery : IRequest<CaseStageDetailsDto?>
    {
        public int CaseId { get; set; }
    }
    public class GetActiveCaseStageQueryHandler : IRequestHandler<GetActiveCaseStageQuery, CaseStageDetailsDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActiveCaseStageQueryHandler> _logger;

        public GetActiveCaseStageQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetActiveCaseStageQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CaseStageDetailsDto?> Handle(GetActiveCaseStageQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب المرحلة النشطة للقضية {CaseId}", request.CaseId);

            var activeStage = await _uow.Repository<CaseStage>()
                .FirstOrDefaultAsync(
                    predicate: cs => cs.CaseId == request.CaseId && cs.IsActive,
                    includeProperties: "Case"
                );

            if (activeStage == null)
            {
                _logger.LogInformation("لا توجد مرحلة نشطة للقضية {CaseId}", request.CaseId);
                return null;
            }

            var caseStageDetails = _mapper.Map<CaseStageDetailsDto>(activeStage);

            return caseStageDetails;
        }
    }
}