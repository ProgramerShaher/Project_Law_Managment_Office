using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetActiveCaseTeamMembers
{
    public class GetActiveCaseTeamMembersQuery : IRequest<List<CaseTeamListDto>>
    {
        public int CaseId { get; set; }
    }

    public class GetActiveCaseTeamMembersQueryHandler : IRequestHandler<GetActiveCaseTeamMembersQuery, List<CaseTeamListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActiveCaseTeamMembersQueryHandler> _logger;

        public GetActiveCaseTeamMembersQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetActiveCaseTeamMembersQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTeamListDto>> Handle(GetActiveCaseTeamMembersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب الأعضاء النشطين لفريق القضية {CaseId}", request.CaseId);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CaseId);

            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            // فلترة للأعضاء النشطين فقط
            Expression<Func<CaseTeam, bool>> filter = ct =>
                ct.CaseId == request.CaseId &&
                ct.IsActive;

            var caseTeams = await _uow.Repository<CaseTeam>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderBy(ct => ct.Role).ThenBy(ct => ct.StartDate),
                    includeProperties: "Lawyer"
                );

            var result = _mapper.Map<List<CaseTeamListDto>>(caseTeams);

            _logger.LogInformation("تم جلب {Count} عضو نشط لفريق القضية {CaseId}", result.Count, request.CaseId);

            return result;
        }
    }
}