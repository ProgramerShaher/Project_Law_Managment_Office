using MediatR;

namespace LawOfficeManagement.Application.Features.Clients.Commands.Delete
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
