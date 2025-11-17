using AutoMapper;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries
{
    public class GetAllCaseSessionsWithDetailsQueryHandler : IRequestHandler<GetAllCaseSessionsWithDetailsQuery, List<CaseSessionWithDetailsDto>>
    {
        private readonly ILogger<GetAllCaseSessionsWithDetailsQueryHandler> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllCaseSessionsWithDetailsQueryHandler(
            ILogger<GetAllCaseSessionsWithDetailsQueryHandler> logger,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CaseSessionWithDetailsDto>> Handle(GetAllCaseSessionsWithDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all case sessions with full details");

                var query = _uow.Repository<CaseSession>()
                    .GetAll()
                    .Where(cs => !cs.IsDeleted)
                    .AsQueryable();

                // تطبيق الفلاتر
                query = ApplyFilters(query, request);

                // تحميل العلاقات
                query = query
                    .Include(cs => cs.Case)
                    .Include(cs => cs.Court)
                    .Include(cs => cs.CourtDivision)
                    .Include(cs => cs.AssignedLawyer);

                // تحميل الأدلة إذا مطلوب
                if (request.IncludeEvidences)
                {
                    query = query.Include(cs => cs.CaseEvidences.Where(e => !e.IsDeleted));
                }

                // تحميل الشهود إذا مطلوب
                if (request.IncludeWitnesses)
                {
                    query = query.Include(cs => cs.CaseWitnesses.Where(w => !w.IsDeleted));
                }

                // تحميل المستندات
                query = query.Include(cs => cs.Documents.Where(d => !d.IsDeleted));

                var sessions = await query
                    .OrderByDescending(cs => cs.SessionDate)
                    .ThenBy(cs => cs.SessionTime)
                    .ToListAsync(cancellationToken);

                var result = new List<CaseSessionWithDetailsDto>();

                foreach (var session in sessions)
                {
                    var sessionDto = new CaseSessionWithDetailsDto
                    {
                        // معلومات الجلسة الأساسية
                        Id = session.Id,
                        CaseId = session.CaseId,
                        CaseNumber = session.Case?.CaseNumber,
                        CaseTitle = session.Case?.Title,
                        CourtId = session.CourtId,
                        CourtName = session.Court?.Name ?? string.Empty,
                        CourtDivisionId = session.CourtDivisionId,
                        CourtDivisionName = session.CourtDivision?.Name ?? string.Empty,
                        AssignedLawyerId = session.AssignedLawyerId,
                        AssignedLawyerName = session.AssignedLawyer?.FullName,
                        SessionDate = session.SessionDate,
                        SessionTime = session.SessionTime,
                        SessionNumber = session.SessionNumber,
                        SessionType = session.SessionType,
                        SessionStatus = session.SessionStatus,
                        Location = session.Location,
                        Notes = session.Notes,
                        Decision = session.Decision,
                        NextSessionDate = session.NextSessionDate,
                        LawyerAttended = session.LawyerAttended,
                        ClientAttended = session.ClientAttended,

                        // معلومات التتبع
                        CreatedAt = session.CreatedAt,
                        CreatedBy = session.CreatedBy,
                        LastModifiedAt = session.LastModifiedAt,
                        LastModifiedBy = session.LastModifiedBy,

                        // تهيئة القوائم
                        Evidences = new List<CaseEvidenceDetailDto>(),
                        Witnesses = new List<CaseWitnessDetailDto>(),
                        Documents = new List<SessionDocumentDto>()
                    };

                    // الأدلة
                    if (request.IncludeEvidences && session.CaseEvidences != null)
                    {
                        foreach (var evidence in session.CaseEvidences.Where(e => !e.IsDeleted))
                        {
                            sessionDto.Evidences.Add(new CaseEvidenceDetailDto
                            {
                                Id = evidence.Id,
                                Title = evidence.Title,
                                EvidenceType = evidence.EvidenceType,
                                Description = evidence.Description,
                                Source = evidence.Source,
                                SubmissionDate = evidence.SubmissionDate,
                                IsAccepted = evidence.IsAccepted,
                                CourtNotes = evidence.CourtNotes,
                                CreatedAt = evidence.CreatedAt,
                                CreatedBy = evidence.CreatedBy
                            });
                        }
                    }

                    // الشهود
                    if (request.IncludeWitnesses && session.CaseWitnesses != null)
                    {
                        foreach (var witness in session.CaseWitnesses.Where(w => !w.IsDeleted))
                        {
                            sessionDto.Witnesses.Add(new CaseWitnessDetailDto
                            {
                                Id = witness.Id,
                                FullName = witness.FullName,
                                NationalId = witness.NationalId,
                                PhoneNumber = witness.PhoneNumber,
                                Address = witness.Address,
                                TestimonySummary = witness.TestimonySummary,
                                IsAttended = witness.IsAttended,
                                TestimonyDate = witness.TestimonyDate,
                                Notes = witness.Notes,
                                CreatedAt = witness.CreatedAt,
                                CreatedBy = witness.CreatedBy
                            });
                        }
                    }

                    // المستندات
                    if (session.Documents != null)
                    {
                        foreach (var document in session.Documents.Where(d => !d.IsDeleted))
                        {
                            sessionDto.Documents.Add(new SessionDocumentDto
                            {
                                Id = document.Id,
                                DocumentName = document.FileName,
                                DocumentPath = document.FilePath,
                                DocumentType = document.FileType,
                                UploadDate = document.CreatedAt
                            });
                        }
                    }

                    result.Add(sessionDto);
                }

                _logger.LogInformation("Retrieved {Count} case sessions with full details", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving case sessions with details");
                throw;
            }
        }

        private IQueryable<CaseSession> ApplyFilters(IQueryable<CaseSession> query, GetAllCaseSessionsWithDetailsQuery request)
        {
            if (request.CaseId.HasValue)
            {
                query = query.Where(cs => cs.CaseId == request.CaseId);
            }

            if (request.CourtId.HasValue)
            {
                query = query.Where(cs => cs.CourtId == request.CourtId);
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(cs => cs.SessionDate >= request.FromDate);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(cs => cs.SessionDate <= request.ToDate);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(cs => cs.SessionStatus == request.Status);
            }

            if (request.LawyerId.HasValue)
            {
                query = query.Where(cs => cs.AssignedLawyerId == request.LawyerId);
            }

            return query;
        }
    }
}