using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Commands.DeleteCourt
{
    public class DeleteCourtCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
