using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using LawOfficeManagement.Core.Interfaces;
using LawOfficeManagement.Core.Common;
using LawOfficeManagement.Application.DTOs;
using System.Linq;
namespace LawOfficeManagement.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(
            UserManager<IdentityUser> userManager,
            ILogger<CreateUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new user with email: {Email}", request.UserDto.Email);

                // تحقق من وجود المستخدم مسبقًا
                var existingUser = await _userManager.FindByEmailAsync(request.UserDto.Email);
                if (existingUser != null)
                {
                    return Result<UserDto>.Failure("User with this email already exists");
                }

                var user = new IdentityUser
                {
                    UserName = request.UserDto.UserName,
                    Email = request.UserDto.Email,
                    PhoneNumber = request.UserDto.PhoneNumber
                };
                // إنشاء المستخدم
                var createResult = await _userManager.CreateAsync(user, request.UserDto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToList();
                    _logger.LogWarning("User creation failed for {Email}. Errors: {Errors}",
                        request.UserDto.Email, string.Join(", ", errors));
                    return Result<UserDto>.Failure(errors);
                }

                // إضافة الدور بناءً على نوع المستخدم
                string role = request.UserDto.UserType.ToString();
                var roleResult = await _userManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    _logger.LogWarning("Role assignment failed for user {Email}", request.UserDto.Email);
                }

                _logger.LogInformation("User created successfully with ID: {UserId}", user.Id);

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = request.UserDto.FirstName,
                    LastName = request.UserDto.LastName,
                    UserName = user.UserName!,
                    BarNumber = request.UserDto.BarNumber,
                    UserType = request.UserDto.UserType,
                    PhoneNumber = user.PhoneNumber ?? "",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                return Result<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with email: {Email}", request.UserDto.Email);
                return Result<UserDto>.Failure("An error occurred while creating user");
            }
        }
    }

    // =============================================
    // LOGIN HANDLER
    // =============================================
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            UserManager<IdentityUser> userManager,
            ITokenService tokenService,
            ILogger<LoginCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}", request.LoginDto.Email);

                var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
                if (user == null)
                {
                    _logger.LogWarning("User not found for email: {Email}", request.LoginDto.Email);
                    return Result<AuthResponseDto>.Failure("Invalid credentials");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.LoginDto.Password);
                if (!isPasswordValid)
                {
                    _logger.LogWarning("Invalid password for email: {Email}", request.LoginDto.Email);
                    return Result<AuthResponseDto>.Failure("Invalid credentials");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.GenerateToken(user.Id, user.Email!, user.UserName!, roles.ToList());

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = "", // يمكن ربطها لاحقًا بجدول معلومات المستخدم
                    LastName = "",
                    UserName = user.UserName!,
                    BarNumber = "",
                    UserType = Core.Enums.UserType.Administrator, // افتراضي مؤقت
                    PhoneNumber = user.PhoneNumber ?? "",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var authResponse = new AuthResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddHours(3),
                    User = userDto,
                    Roles = roles.ToList()
                };

                _logger.LogInformation("Successful login for user: {Email}", request.LoginDto.Email);
                return Result<AuthResponseDto>.Success(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", request.LoginDto.Email);
                return Result<AuthResponseDto>.Failure("An error occurred during login");
            }
        }
    }
}