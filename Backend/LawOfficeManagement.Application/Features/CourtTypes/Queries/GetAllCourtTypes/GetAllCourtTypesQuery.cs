using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Queries.GetAllCourtTypes
{
    public class GetAllCourtTypesQuery : IRequest<List<CourtTypeDto>> { }
}
