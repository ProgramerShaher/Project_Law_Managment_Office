using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.Cases.Commands.Dtos
{
    public class CreateCaseSessionDto
    {
        public int? CaseId { get; set; }
        public int CourtId { get; set; }
        public int CourtDivisionId { get; set; }
        public int? AssignedLawyerId { get; set; }
        public DateTime SessionDate { get; set; }
        public string? SessionTime { get; set; }
        public string? SessionNumber { get; set; }
        public string? SessionType { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public List<CreateCaseEvidenceDto> Evidences { get; set; } = new();
        public List<CreateCaseWitnessDto> Witnesses { get; set; } = new();
    }

    public class CreateCaseEvidenceDto
    {
        public string Title { get; set; } = string.Empty;
        public string? EvidenceType { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }

    public class CreateCaseWitnessDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? TestimonySummary { get; set; }
    }
}