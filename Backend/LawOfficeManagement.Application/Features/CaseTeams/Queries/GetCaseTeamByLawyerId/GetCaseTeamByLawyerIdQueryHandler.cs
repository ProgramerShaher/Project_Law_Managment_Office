using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByLawyerId
{
    public class GetCaseTeamByLawyerIdQuery : IRequest<List<CaseTeamLawyerDto>>
    {
        public int LawyerId { get; set; }
        public bool IncludeInactive { get; set; } = false;
    }
    public class GetCaseTeamByLawyerIdQueryHandler : IRequestHandler<GetCaseTeamByLawyerIdQuery, List<CaseTeamLawyerDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseTeamByLawyerIdQueryHandler> _logger;

        public GetCaseTeamByLawyerIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseTeamByLawyerIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTeamLawyerDto>> Handle(GetCaseTeamByLawyerIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب قضايا المحامي {LawyerId} - تضمين غير النشطة: {IncludeInactive}",
                request.LawyerId, request.IncludeInactive);

            // التحقق من وجود المحامي
            var lawyerExists = await _uow.Repository<Lawyer>()
                .ExistsAsync(l => l.Id == request.LawyerId && !l.IsDeleted);
            if (!lawyerExists)
                throw new InvalidOperationException("المحامي غير موجود");

            System.Linq.Expressions.Expression<Func<CaseTeam, bool>> filter;

            if (request.IncludeInactive)
            {
                filter = ct => ct.LawyerId == request.LawyerId;
            }
            else
            {
                filter = ct => ct.LawyerId == request.LawyerId && ct.IsActive;
            }

            var caseTeams = await _uow.Repository<CaseTeam>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(ct => ct.StartDate),
                    includeProperties: "Case"
                );

            var result = _mapper.Map<List<CaseTeamLawyerDto>>(caseTeams);

            _logger.LogInformation("تم جلب {Count} قضية للمحامي {LawyerId}", result.Count, request.LawyerId);

            return result;
        }
    }
}