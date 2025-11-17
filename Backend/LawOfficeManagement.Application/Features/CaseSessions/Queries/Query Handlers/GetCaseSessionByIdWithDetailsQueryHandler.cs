using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries
{
    public class GetCaseSessionByIdWithDetailsQueryHandler : IRequestHandler<GetCaseSessionByIdWithDetailsQuery, CaseSessionWithDetailsDto>
    {
        private readonly ILogger<GetCaseSessionByIdWithDetailsQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCaseSessionByIdWithDetailsQueryHandler(
            ILogger<GetCaseSessionByIdWithDetailsQueryHandler> logger,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CaseSessionWithDetailsDto> Handle(GetCaseSessionByIdWithDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving case session with full details for ID: {SessionId}", request.Id);

                var caseSession = await _uow.Repository<CaseSession>()
                    .GetAll()
                    .Where(cs => cs.Id == request.Id && !cs.IsDeleted)
                    .Include(cs => cs.Case)
                    .Include(cs => cs.Court)
                    .Include(cs => cs.CourtDivision)
                    .Include(cs => cs.AssignedLawyer)
                    .Include(cs => cs.CaseEvidences.Where(e => !e.IsDeleted))
                    .Include(cs => cs.CaseWitnesses.Where(w => !w.IsDeleted))
                    .Include(cs => cs.Documents.Where(d => !d.IsDeleted))
                    .FirstOrDefaultAsync(cancellationToken);

                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                var result = new CaseSessionWithDetailsDto
                {
                    // معلومات الجلسة الأساسية
                    Id = caseSession.Id,
                    CaseId = caseSession.CaseId,
                    CaseNumber = caseSession.Case?.CaseNumber,
                    CaseTitle = caseSession.Case?.Title,
                    CourtId = caseSession.CourtId,
                    CourtName = caseSession.Court?.Name ?? string.Empty,
                    CourtDivisionId = caseSession.CourtDivisionId,
                    CourtDivisionName = caseSession.CourtDivision?.Name ?? string.Empty,
                    AssignedLawyerId = caseSession.AssignedLawyerId,
                    AssignedLawyerName = caseSession.AssignedLawyer?.FullName,
                    SessionDate = caseSession.SessionDate,
                    SessionTime = caseSession.SessionTime,
                    SessionNumber = caseSession.SessionNumber,
                    SessionType = caseSession.SessionType,
                    SessionStatus = caseSession.SessionStatus,
                    Location = caseSession.Location,
                    Notes = caseSession.Notes,
                    Decision = caseSession.Decision,
                    NextSessionDate = caseSession.NextSessionDate,
                    LawyerAttended = caseSession.LawyerAttended,
                    ClientAttended = caseSession.ClientAttended,

                    // معلومات التتبع
                    CreatedAt = caseSession.CreatedAt,
                    CreatedBy = caseSession.CreatedBy,
                    LastModifiedAt = caseSession.LastModifiedAt,
                    LastModifiedBy = caseSession.LastModifiedBy,

                    // الأدلة
                    Evidences = caseSession.CaseEvidences?
                        .Select(e => new CaseEvidenceDetailDto
                        {
                            Id = e.Id,
                            Title = e.Title,
                            EvidenceType = e.EvidenceType,
                            Description = e.Description,
                            Source = e.Source,
                            SubmissionDate = e.SubmissionDate,
                            IsAccepted = e.IsAccepted,
                            CourtNotes = e.CourtNotes,
                            CreatedAt = e.CreatedAt,
                            CreatedBy = e.CreatedBy
                        }).ToList() ?? new List<CaseEvidenceDetailDto>(),

                    // الشهود
                    Witnesses = caseSession.CaseWitnesses?
                        .Select(w => new CaseWitnessDetailDto
                        {
                            Id = w.Id,
                            FullName = w.FullName,
                            NationalId = w.NationalId,
                            PhoneNumber = w.PhoneNumber,
                            Address = w.Address,
                            TestimonySummary = w.TestimonySummary,
                            IsAttended = w.IsAttended,
                            TestimonyDate = w.TestimonyDate,
                            Notes = w.Notes,
                            CreatedAt = w.CreatedAt,
                            CreatedBy = w.CreatedBy
                        }).ToList() ?? new List<CaseWitnessDetailDto>(),

                    // المستندات
                    Documents = caseSession.Documents?
                        .Select(d => new SessionDocumentDto
                        {
                            Id = d.Id,
                            DocumentName = d.FileName,
                            DocumentPath = d.FilePath,
                            DocumentType = d.FileType,
                            UploadDate = d.CreatedAt
                            //Description = d.
                        }).ToList() ?? new List<SessionDocumentDto>()
                };

                _logger.LogInformation("Successfully retrieved case session with full details for ID: {SessionId}", request.Id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving case session with details for ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}