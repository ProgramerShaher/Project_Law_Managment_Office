using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamMembersWithDetails
{
     public class GetCaseTeamMembersWithDetailsQuery : IRequest<List<CaseTeamMemberDetailsDto>>
    {
        public int CaseId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
    public class GetCaseTeamMembersWithDetailsQueryHandler
        : IRequestHandler<GetCaseTeamMembersWithDetailsQuery, List<CaseTeamMemberDetailsDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseTeamMembersWithDetailsQueryHandler> _logger;

        public GetCaseTeamMembersWithDetailsQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseTeamMembersWithDetailsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTeamMemberDetailsDto>> Handle(
            GetCaseTeamMembersWithDetailsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب أعضاء فريق القضية {CaseId} مع التفاصيل", request.CaseId);

            // بناء التعبير للتصفية
            System.Linq.Expressions.Expression<Func<CaseTeam, bool>> filter;

            if (request.IncludeInactive)
            {
                filter = ct => ct.CaseId == request.CaseId;
            }
            else
            {
                filter = ct => ct.CaseId == request.CaseId && ct.IsActive;
            }

            var caseTeamMembers = await _uow.Repository<CaseTeam>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderBy(ct => ct.Role).ThenBy(ct => ct.StartDate),
                    includeProperties: "Lawyer,DerivedPowerOfAttorney"
                );

            var result = new List<CaseTeamMemberDetailsDto>();

            foreach (var member in caseTeamMembers)
            {
                var memberDto = _mapper.Map<CaseTeamMemberDetailsDto>(member);


                result.Add(memberDto);
            }

            _logger.LogInformation("تم جلب {Count} عضو لفريق القضية {CaseId}", result.Count, request.CaseId);

            return result;
        }
    }
}