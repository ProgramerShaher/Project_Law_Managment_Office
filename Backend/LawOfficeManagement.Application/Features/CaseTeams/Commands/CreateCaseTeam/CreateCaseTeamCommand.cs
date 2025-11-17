using MediatR;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.CreateCaseTeam
{
    public class CreateCaseTeamCommand : IRequest<int>
    {
        public CreateCaseTeamDto CreateDto { get; set; } = null!;
    }

    public class CreateCaseTeamDto
    {
        public int LawyerId { get; set; }
        public int CaseId { get; set; }
        public string Role { get; set; } = "مساعد";
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateCaseTeamResponse
    {
        public int CaseTeamId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
      
    }
}