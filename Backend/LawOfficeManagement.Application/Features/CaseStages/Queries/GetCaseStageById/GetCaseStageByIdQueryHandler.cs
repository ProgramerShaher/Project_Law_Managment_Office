using AutoMapper;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetCaseStageById
{
    public class GetCaseStageByIdQuery : IRequest<CaseStageDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetCaseStageByIdQueryHandler : IRequestHandler<GetCaseStageByIdQuery, CaseStageDetailsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseStageByIdQueryHandler> _logger;

        public GetCaseStageByIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseStageByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CaseStageDetailsDto> Handle(GetCaseStageByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب تفاصيل المرحلة {StageId}", request.Id);

            var caseStage = await _uow.Repository<CaseStage>()
                .FirstOrDefaultAsync(
                    predicate: cs => cs.Id == request.Id,
                    includeProperties: "Case"
                );

            if (caseStage == null)
                throw new InvalidOperationException("المرحلة غير موجودة");

            var caseStageDetails = _mapper.Map<CaseStageDetailsDto>(caseStage);

            return caseStageDetails;
        }
    }
}