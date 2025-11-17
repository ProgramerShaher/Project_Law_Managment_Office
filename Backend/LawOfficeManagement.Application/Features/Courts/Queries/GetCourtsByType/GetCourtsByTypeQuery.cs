using LawOfficeManagement.Application.Features.Courts.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtsByType
{
    public class GetCourtsByTypeQuery : IRequest<IEnumerable<CourtDto>>
    {
        public int CourtTypeId { get; set; }
    }
}