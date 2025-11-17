using LawOfficeManagement.Application.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Queries.GetClientRoleById
{
    public class GetClientRoleByIdQuery : IRequest<ClientRoleDto?>
    {
        public int Id { get; set; }
    }
}
