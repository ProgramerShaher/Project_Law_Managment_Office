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
    public class GetAllCaseSessionsQueryHandler : IRequestHandler<GetAllCaseSessionsQuery, List<CaseSessionDto>>
    {
        private readonly ILogger<GetAllCaseSessionsQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllCaseSessionsQueryHandler(
            ILogger<GetAllCaseSessionsQueryHandler> logger,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CaseSessionDto>> Handle(GetAllCaseSessionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all case sessions with filters");

                var query = _uow.Repository<CaseSession>()
                    .GetAll()
                    .Include(cs => cs.Case)
                    .Include(cs => cs.Court)
                    .Include(cs => cs.CourtDivision)
                    .Include(cs => cs.AssignedLawyer)
                    .Include(cs => cs.CaseEvidences)
                    .Include(cs => cs.CaseWitnesses)
                    .Where(cs => !cs.IsDeleted)
                    .AsQueryable();

                // تطبيق الفلاتر
                if (request.CaseId.HasValue)
                    query = query.Where(cs => cs.CaseId == request.CaseId);

                if (request.CourtId.HasValue)
                    query = query.Where(cs => cs.CourtId == request.CourtId);

                if (request.FromDate.HasValue)
                    query = query.Where(cs => cs.SessionDate >= request.FromDate);

                if (request.ToDate.HasValue)
                    query = query.Where(cs => cs.SessionDate <= request.ToDate);

                if (request.Status.HasValue)
                    query = query.Where(cs => cs.SessionStatus == request.Status);

                if (request.LawyerId.HasValue)
                    query = query.Where(cs => cs.AssignedLawyerId == request.LawyerId);

                var result = await query
                    .OrderByDescending(cs => cs.SessionDate)
                    .ThenBy(cs => cs.SessionTime)
                    .ProjectTo<CaseSessionDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Retrieved {Count} case sessions", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving case sessions");
                throw;
            }
        }
    }
}