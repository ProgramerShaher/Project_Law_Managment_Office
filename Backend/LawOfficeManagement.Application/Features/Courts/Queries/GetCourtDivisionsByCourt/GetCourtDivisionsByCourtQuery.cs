using LawOfficeManagement.Application.Features.Courts.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtDivisionsByCourt
{
    public class GetCourtDivisionsByCourtQuery : IRequest<IEnumerable<CourtDivisionDto>>
    {
        public int CourtId { get; set; }
    }
}