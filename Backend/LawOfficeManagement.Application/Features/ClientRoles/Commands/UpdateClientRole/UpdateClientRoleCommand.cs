using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.UpdateClientRole
{
    public class UpdateClientRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
