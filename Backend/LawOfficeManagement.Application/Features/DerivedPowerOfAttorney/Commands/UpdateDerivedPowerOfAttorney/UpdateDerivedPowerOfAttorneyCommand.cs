using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.UpdateDerivedPowerOfAttorney
{
    public class UpdateDerivedPowerOfAttorneyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public CreateDerivedPowerOfAttorneyDto UpdateDto { get; set; }
    }
}