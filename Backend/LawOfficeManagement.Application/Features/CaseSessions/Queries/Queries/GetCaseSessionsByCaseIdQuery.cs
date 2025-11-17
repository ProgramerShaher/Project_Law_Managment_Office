using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries.Queries
{
    public class GetCaseSessionsByCaseIdQuery : IRequest<List<CaseSessionDto>>
    {
        public int CaseId { get; set; }
    }
}