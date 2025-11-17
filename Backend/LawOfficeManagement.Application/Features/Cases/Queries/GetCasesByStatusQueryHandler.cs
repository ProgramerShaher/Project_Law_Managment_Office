using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.Cases.Queries
{
    public class GetCasesByStatusQuery : IRequest<List<CaseListDto>>
    {
        public CaseStatus Status { get; set; }
        public bool IncludeArchived { get; set; } = false;
    }

    public class GetCasesByStatusQueryHandler : IRequestHandler<GetCasesByStatusQuery, List<CaseListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCasesByStatusQueryHandler> _logger;

        public GetCasesByStatusQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCasesByStatusQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseListDto>> Handle(GetCasesByStatusQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب القضايا بحالة {Status}", request.Status);

            Expression<Func<Case, bool>> filter = c => c.Status == request.Status;

            if (!request.IncludeArchived)
            {
                filter = c => c.Status == request.Status && !c.IsArchived;
            }

            var cases = await _uow.Repository<Case>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.CreatedAt),
                    includeProperties: "Client,Court,CaseType"
                );

            var caseList = _mapper.Map<List<CaseListDto>>(cases);

            _logger.LogInformation("تم جلب {Count} قضية بحالة {Status}", caseList.Count, request.Status);

            return caseList;
        }
    }
}