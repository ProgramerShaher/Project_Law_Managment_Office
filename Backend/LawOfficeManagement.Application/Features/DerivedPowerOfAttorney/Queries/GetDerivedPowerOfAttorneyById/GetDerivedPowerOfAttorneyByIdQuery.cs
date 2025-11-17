using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetDerivedPowerOfAttorneyById
{
    public class GetDerivedPowerOfAttorneyByIdQuery : IRequest<DerivedPowerOfAttorneyDto>
    {
        public int Id { get; set; }
    }
}