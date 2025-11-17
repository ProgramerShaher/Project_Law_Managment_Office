using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.DeleteCourtType
{
    public class DeleteCourtTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
