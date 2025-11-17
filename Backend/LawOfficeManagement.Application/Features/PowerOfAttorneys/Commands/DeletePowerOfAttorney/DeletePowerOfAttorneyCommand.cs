using MediatR;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.DeletePowerOfAttorney
{
    public class DeletePowerOfAttorneyCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeletePowerOfAttorneyCommand(int id)
        {
            Id = id;
        }
    }
}
