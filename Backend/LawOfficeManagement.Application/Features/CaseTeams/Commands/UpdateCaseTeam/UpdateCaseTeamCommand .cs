using MediatR;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.UpdateCaseTeam
{
    public class UpdateCaseTeamCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateCaseTeamDto UpdateDto { get; set; } = null!;
    }

    public class UpdateCaseTeamDto
    {
        public string Role { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
    //public class UpdateCaseTeamResponse
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; } = string.Empty;
    //    public bool HasDerivedPowerOfAttorney { get; set; }
    //    public string? DerivedPowerOfAttorneyNumber { get; set; }
    //}
}