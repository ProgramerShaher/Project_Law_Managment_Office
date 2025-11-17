using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.CaseSessions.Queries
{
    public class GetCaseSessionByIdWithDetailsQuery : IRequest<CaseSessionWithDetailsDto>
    {
        public int Id { get; set; }
    }
}