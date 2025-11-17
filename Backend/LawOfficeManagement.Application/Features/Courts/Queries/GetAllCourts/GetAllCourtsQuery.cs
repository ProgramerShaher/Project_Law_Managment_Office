using LawOfficeManagement.Application.Features.Courts.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetAllCourts
{
    public class GetAllCaseTypeQuery : IRequest<List<CourtDto>> { }
}
