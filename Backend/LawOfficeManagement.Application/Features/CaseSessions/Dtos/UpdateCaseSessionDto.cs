using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.CaseSessions.Dtos
{
    public class UpdateCaseSessionDto
    {
        public int CourtId { get; set; }
        public int CourtDivisionId { get; set; }
        public int? AssignedLawyerId { get; set; }
        public DateTime SessionDate { get; set; }
        public string? SessionTime { get; set; }
        public string? SessionNumber { get; set; }
        public string? SessionType { get; set; }

        // إذا جعلته nullable في الكيان، اجعله nullable هنا أيضاً
        public CaseSessionStatus SessionStatus { get; set; }
        // أو: public CaseSessionStatus? SessionStatus { get; set; }

        public string? Location { get; set; }
        public string? Notes { get; set; }
        public string? Decision { get; set; }
        public DateTime? NextSessionDate { get; set; }
        public bool LawyerAttended { get; set; }
        public bool ClientAttended { get; set; }
    }
}