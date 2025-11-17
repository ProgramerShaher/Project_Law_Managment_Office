using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetAllCaseTeams
{
    public class GetAllCaseTeamsQueryHandler : IRequestHandler<GetAllCaseTeamsQuery, List<CaseTeamDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCaseTeamsQueryHandler> _logger;

        public GetAllCaseTeamsQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetAllCaseTeamsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTeamDto>> Handle(GetAllCaseTeamsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع فرق العمل مع التصفية - CaseId: {CaseId}, LawyerId: {LawyerId}",
                request.CaseId, request.LawyerId);

            // بناء التعبير للتصفية
            Expression<Func<CaseTeam, bool>>? filter = BuildFilterExpression(request);

            // جلب البيانات باستخدام الـ Repository
            var caseTeams = await _uow.Repository<CaseTeam>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(ct => ct.CreatedAt),
                    includeProperties: "Lawyer,Case,DerivedPowerOfAttorney"
                );

            var result = _mapper.Map<List<CaseTeamDto>>(caseTeams);

            _logger.LogInformation("تم جلب {Count} سجل لفريق العمل", result.Count);

            return result;
        }

        private Expression<Func<CaseTeam, bool>>? BuildFilterExpression(GetAllCaseTeamsQuery request)
        {
            // استخدام الطريقة المبسطة للتصفية
            return ct =>
                (!request.CaseId.HasValue || ct.CaseId == request.CaseId.Value) &&
                (!request.LawyerId.HasValue || ct.LawyerId == request.LawyerId.Value) &&
                (!request.IsActive.HasValue || ct.IsActive == request.IsActive.Value)
          ;
        }
    }
}