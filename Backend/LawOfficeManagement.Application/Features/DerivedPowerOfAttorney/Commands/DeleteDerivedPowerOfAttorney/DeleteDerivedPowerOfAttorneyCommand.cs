using MediatR;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.DeleteDerivedPowerOfAttorney
{
    public class DeleteDerivedPowerOfAttorneyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}