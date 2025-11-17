using MediatR;
using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Core.Common;

namespace LawOfficeManagement.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<Result<UserDto>>
    {
        public CreateUserDto UserDto { get; set; } = null!;
    }

    public class LoginCommand : IRequest<Result<AuthResponseDto>>
    {
        public LoginDto LoginDto { get; set; } = null!;
    }

    public class AssignRoleCommand : IRequest<Result>
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
