using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamById
{
    public class GetCaseTeamByIdQuery : IRequest<CaseTeamDetailsDto>
    {
        public int Id { get; set; }
    }
    public class GetCaseTeamByIdQueryHandler : IRequestHandler<GetCaseTeamByIdQuery, CaseTeamDetailsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseTeamByIdQueryHandler> _logger;

        public GetCaseTeamByIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseTeamByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CaseTeamDetailsDto> Handle(GetCaseTeamByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب تفاصيل فريق العمل {TeamId}", request.Id);

            var caseTeam = await _uow.Repository<CaseTeam>()
                .FirstOrDefaultAsync(
                    predicate: ct => ct.Id == request.Id,
                    includeProperties: "Lawyer,Case,DerivedPowerOfAttorney"
                );

            if (caseTeam == null)
                throw new InvalidOperationException("سجل فريق العمل غير موجود");

            var caseTeamDetails = _mapper.Map<CaseTeamDetailsDto>(caseTeam);

            return caseTeamDetails;
        }
    }
}