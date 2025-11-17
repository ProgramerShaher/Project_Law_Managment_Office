using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
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
    public class GetCaseSessionByIdQueryHandler : IRequestHandler<GetCaseSessionByIdQuery, CaseSessionDto>
    {
        private readonly ILogger<GetCaseSessionByIdQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCaseSessionByIdQueryHandler(
            ILogger<GetCaseSessionByIdQueryHandler> logger,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CaseSessionDto> Handle(GetCaseSessionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving case session with ID: {SessionId}", request.Id);

                var caseSession = await _uow.Repository<CaseSession>()
                    .GetAll()
                    .Include(cs => cs.Case)
                    .Include(cs => cs.Court)
                    .Include(cs => cs.CourtDivision)
                    .Include(cs => cs.AssignedLawyer)
                    .Include(cs => cs.CaseEvidences)
                    .Include(cs => cs.CaseWitnesses)
                    .Include(cs => cs.Documents)
                    .FirstOrDefaultAsync(cs => cs.Id == request.Id && !cs.IsDeleted, cancellationToken);

                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                var result = _mapper.Map<CaseSessionDto>(caseSession);

                _logger.LogInformation("Successfully retrieved case session with ID: {SessionId}", request.Id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}