using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Queries.GetCourtTypeById
{
    public class GetCourtTypeByIdQuery : IRequest<CourtTypeDto?>
    {
        public int Id { get; set; }
    }
}
