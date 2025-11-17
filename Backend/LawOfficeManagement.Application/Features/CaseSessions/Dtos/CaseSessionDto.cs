using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.Cases.Queries.Dtos
{
    public class CaseSessionDto
    {
        public int Id { get; set; }
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
        public List<CaseEvidenceDto> Evidences { get; set; } = new();
        public List<CaseWitnessDto> Witnesses { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CaseEvidenceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? EvidenceType { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public bool IsAccepted { get; set; }
        public string? CourtNotes { get; set; }
    }

    public class CaseWitnessDto
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
    }
}