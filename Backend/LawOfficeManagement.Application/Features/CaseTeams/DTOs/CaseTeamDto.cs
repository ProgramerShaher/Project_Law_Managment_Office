namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetAllCaseTeams
{
    public class CaseTeamDto
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public string? LawyerEmail { get; set; }
        public string? LawyerPhone { get; set; }
        public int CaseId { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string CaseNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
     
    }
}