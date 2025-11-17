using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries
{
    public class GetAllCaseSessionsWithDetailsQuery : IRequest<List<CaseSessionWithDetailsDto>>
    {
        public int? CaseId { get; set; }
        public int? CourtId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Core.Enums.CaseSessionStatus? Status { get; set; }
        public int? LawyerId { get; set; }
        public bool IncludeEvidences { get; set; } = true;
        public bool IncludeWitnesses { get; set; } = true;
    }
}