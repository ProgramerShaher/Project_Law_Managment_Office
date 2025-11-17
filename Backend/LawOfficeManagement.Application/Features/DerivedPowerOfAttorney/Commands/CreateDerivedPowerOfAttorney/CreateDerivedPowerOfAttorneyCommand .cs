using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.CreateDerivedPowerOfAttorney
{
    public class CreateDerivedPowerOfAttorneyCommand : IRequest<int>
    {
        public CreateDerivedPowerOfAttorneyDto CreateDto { get; set; }
    }
}