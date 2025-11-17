using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.DeleteClientRole
{
    public class DeleteClientRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
