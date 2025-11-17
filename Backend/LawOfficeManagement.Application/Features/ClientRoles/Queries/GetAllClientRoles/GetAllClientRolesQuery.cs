using LawOfficeManagement.Application.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Queries.GetAllClientRoles
{
    public class GetAllClientRolesQuery : IRequest<List<ClientRoleDto>> { }
}
