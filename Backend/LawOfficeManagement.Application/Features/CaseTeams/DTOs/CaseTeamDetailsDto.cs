namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamById
{
    public class CaseTeamDetailsDto
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public int CaseId { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string CaseNumber { get; set; } = string.Empty;
   
        public string Role { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}