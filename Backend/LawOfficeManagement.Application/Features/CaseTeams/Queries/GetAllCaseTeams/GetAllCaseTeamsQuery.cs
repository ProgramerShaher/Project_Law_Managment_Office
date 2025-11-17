using MediatR;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetAllCaseTeams
{
    public class GetAllCaseTeamsQuery : IRequest<List<CaseTeamDto>>
    {
        public int? CaseId { get; set; }
        public int? LawyerId { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasDerivedPowerOfAttorney { get; set; }
    }
}