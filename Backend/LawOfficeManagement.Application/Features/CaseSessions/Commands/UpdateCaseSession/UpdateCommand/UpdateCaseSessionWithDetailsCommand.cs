using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionWithDetailsCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCaseSessionDto UpdateCaseSessionDto { get; set; } = new();
        public List<UpdateCaseEvidenceDto> Evidences { get; set; } = new();
        public List<UpdateCaseWitnessDto> Witnesses { get; set; } = new();
    }

    public class UpdateCaseEvidenceDto
    {
        public int Id { get; set; } // 0 for new, >0 for existing
        public string Title { get; set; } = string.Empty;
        public string? EvidenceType { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public bool IsAccepted { get; set; }
        public string? CourtNotes { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdateCaseWitnessDto
    {
        public int Id { get; set; } // 0 for new, >0 for existing
        public string FullName { get; set; } = string.Empty;
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? TestimonySummary { get; set; }
        public bool IsAttended { get; set; }
        public DateTime? TestimonyDate { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}