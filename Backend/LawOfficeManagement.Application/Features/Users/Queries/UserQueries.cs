using MediatR;
using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Core.Common;

namespace LawOfficeManagement.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserDto>>
    {
        public string UserId { get; set; } = string.Empty;
    }

    public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {
    }

    public class GetUserRolesQuery : IRequest<Result<List<string>>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
