using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetAllDerivedPowerOfAttorneys
{
    public class GetAllDerivedPowerOfAttorneysQuery : IRequest<IEnumerable<DerivedPowerOfAttorneyDto>>
    {
    }
}