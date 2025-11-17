using AutoMapper;
using AutoMapper.QueryableExtensions;
using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Queries.Queries;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries
{
    public class GetCaseSessionsByCaseIdQueryHandler : IRequestHandler<GetCaseSessionsByCaseIdQuery, List<CaseSessionDto>>
    {
        private readonly ILogger<GetCaseSessionsByCaseIdQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCaseSessionsByCaseIdQueryHandler(
            ILogger<GetCaseSessionsByCaseIdQueryHandler> logger,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CaseSessionDto>> Handle(GetCaseSessionsByCaseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving case sessions for case ID: {CaseId}", request.CaseId);

                var caseSessions = await _uow.Repository<CaseSession>()
                    .GetAll()
                    .Where(cs => cs.CaseId == request.CaseId && !cs.IsDeleted)
                    .Include(cs => cs.Case)
                    .Include(cs => cs.Court)
                    .Include(cs => cs.CourtDivision)
                    .Include(cs => cs.AssignedLawyer)
                    .Include(cs => cs.CaseEvidences)
                    .Include(cs => cs.CaseWitnesses)
                    .OrderByDescending(cs => cs.SessionDate)
                    .ThenBy(cs => cs.SessionTime)
                    .ProjectTo<CaseSessionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Retrieved {Count} case sessions for case ID: {CaseId}",
                    caseSessions.Count, request.CaseId);

                return caseSessions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving case sessions for case ID: {CaseId}", request.CaseId);
                throw;
            }
        }
    }
}