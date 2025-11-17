using LawOfficeManagement.Application.Features.Courts.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtById
{
    public class GetCourtByIdQuery : IRequest<CourtDto?>
    {
        public int Id { get; set; }
    }
}
