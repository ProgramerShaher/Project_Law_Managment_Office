using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByCaseId
{
    public class GetCaseTeamByCaseIdQuery : IRequest<List<CaseTeamListDto>>
    {
        public int CaseId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
    public class GetCaseTeamByCaseIdQueryHandler : IRequestHandler<GetCaseTeamByCaseIdQuery, List<CaseTeamListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseTeamByCaseIdQueryHandler> _logger;

        public GetCaseTeamByCaseIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseTeamByCaseIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTeamListDto>> Handle(GetCaseTeamByCaseIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب فريق عمل القضية {CaseId} - تضمين غير النشطين: {IncludeInactive}",
                request.CaseId, request.IncludeInactive);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CaseId);
            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            System.Linq.Expressions.Expression<Func<CaseTeam, bool>> filter;

            if (request.IncludeInactive)
            {
                filter = ct => ct.CaseId == request.CaseId;
            }
            else
            {
                filter = ct => ct.CaseId == request.CaseId && ct.IsActive;
            }

            var caseTeams = await _uow.Repository<CaseTeam>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderBy(ct => ct.Role).ThenBy(ct => ct.StartDate),
                    includeProperties: "Lawyer"
                );

            var result = _mapper.Map<List<CaseTeamListDto>>(caseTeams);

        

            _logger.LogInformation("تم جلب {Count} عضو لفريق القضية {CaseId}", result.Count, request.CaseId);

            return result;
        }
    }
}