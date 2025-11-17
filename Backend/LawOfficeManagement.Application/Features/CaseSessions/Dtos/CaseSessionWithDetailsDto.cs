using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.CaseSessions.Dtos
{
    public class CaseSessionWithDetailsDto
    {
        public int Id { get; set; }

        // معلومات الجلسة الأساسية
        public int? CaseId { get; set; }
        public string? CaseNumber { get; set; }
        public string? CaseTitle { get; set; }
        public int CourtId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public int CourtDivisionId { get; set; }
        public string CourtDivisionName { get; set; } = string.Empty;
        public int? AssignedLawyerId { get; set; }
        public string? AssignedLawyerName { get; set; }
        public DateTime SessionDate { get; set; }
        public string? SessionTime { get; set; }
        public string? SessionNumber { get; set; }
        public string? SessionType { get; set; }
        public CaseSessionStatus SessionStatus { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public string? Decision { get; set; }
        public DateTime? NextSessionDate { get; set; }
        public bool LawyerAttended { get; set; }
        public bool ClientAttended { get; set; }

        // معلومات إضافية
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;

        // البيانات المرتبطة
        public List<CaseEvidenceDetailDto> Evidences { get; set; } = new();
        public List<CaseWitnessDetailDto> Witnesses { get; set; } = new();
        public List<SessionDocumentDto> Documents { get; set; } = new();
    }

    public class CaseEvidenceDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? EvidenceType { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public bool IsAccepted { get; set; }
        public string? CourtNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CaseWitnessDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? TestimonySummary { get; set; }
        public bool IsAttended { get; set; }
        public DateTime? TestimonyDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class SessionDocumentDto
    {
        public int Id { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string? DocumentPath { get; set; }
        public string? DocumentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string? Description { get; set; }
    }
}