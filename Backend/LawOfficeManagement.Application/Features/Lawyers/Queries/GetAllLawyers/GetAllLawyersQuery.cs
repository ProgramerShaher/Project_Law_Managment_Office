using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Lawyers.Queries.GetAllLawyers
{
    public class GetAllLawyersQuery : IRequest<IReadOnlyList<LawyerDto>>
    {
    }
}
