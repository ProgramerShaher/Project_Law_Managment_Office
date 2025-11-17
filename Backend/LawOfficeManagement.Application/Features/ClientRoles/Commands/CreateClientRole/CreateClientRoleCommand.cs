using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.CreateClientRole
{
    public class CreateClientRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
