using AutoMapper;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetCaseStagesByCaseId
{
    public class GetCaseStagesByCaseIdQuery : IRequest<List<CaseStageListDto>>
    {
        public int CaseId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
    public class GetCaseStagesByCaseIdQueryHandler : IRequestHandler<GetCaseStagesByCaseIdQuery, List<CaseStageListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseStagesByCaseIdQueryHandler> _logger;

        public GetCaseStagesByCaseIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseStagesByCaseIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseStageListDto>> Handle(GetCaseStagesByCaseIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب مراحل القضية {CaseId} - تضمين غير النشطة: {IncludeInactive}",
                request.CaseId, request.IncludeInactive);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CaseId);

            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            System.Linq.Expressions.Expression<Func<CaseStage, bool>> filter;

            if (request.IncludeInactive)
            {
                filter = cs => cs.CaseId == request.CaseId;
            }
            else
            {
                filter = cs => cs.CaseId == request.CaseId && cs.IsActive;
            }

            var caseStages = await _uow.Repository<CaseStage>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderBy(cs => cs.CreatedAt),
                    includeProperties: "Case"
                );

            var caseStageList = _mapper.Map<List<CaseStageListDto>>(caseStages);

            _logger.LogInformation("تم جلب {Count} مرحلة للقضية {CaseId}", caseStageList.Count, request.CaseId);

            return caseStageList;
        }
    }
}