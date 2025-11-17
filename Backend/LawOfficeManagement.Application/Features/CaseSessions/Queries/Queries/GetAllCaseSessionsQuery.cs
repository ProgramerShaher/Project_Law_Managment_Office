using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using LawOfficeManagement.Core.Enums;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries.Queries
{
    public class GetAllCaseSessionsQuery : IRequest<List<CaseSessionDto>>
    {
        public int? CaseId { get; set; }
        public int? CourtId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public CaseSessionStatus? Status { get; set; }
        public int? LawyerId { get; set; }

    }
}