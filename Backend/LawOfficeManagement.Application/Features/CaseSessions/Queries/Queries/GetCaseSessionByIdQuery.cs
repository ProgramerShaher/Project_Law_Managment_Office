using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries.Queries
{
    public class GetCaseSessionByIdQuery : IRequest<CaseSessionDto>
    {
        public int Id { get; set; }
    }
}