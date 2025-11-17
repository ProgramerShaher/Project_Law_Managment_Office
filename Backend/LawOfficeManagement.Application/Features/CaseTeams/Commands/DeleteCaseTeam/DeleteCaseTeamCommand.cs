using MediatR;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.DeleteCaseTeam
{
    public class DeleteCaseTeamCommand : IRequest<DeleteCaseTeamResponse>
    {
        public int Id { get; set; }
        public string? Reason { get; set; }
    }

    public class DeleteCaseTeamResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}