using Microsoft.AspNetCore.Mvc;
using MediatR;
using LawOfficeManagement.Application.Features.Users.Commands;
using LawOfficeManagement.Application.Features.Users.Queries;
using LawOfficeManagement.Application.DTOs;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var command = new CreateUserCommand { UserDto = createUserDto };
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("User registered successfully: {Email}", createUserDto.Email);
                    return Ok(result.Data);
                }

                _logger.LogWarning("User registration failed: {Error}", result.Error);
                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var command = new LoginCommand { LoginDto = loginDto };
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("User logged in successfully: {Email}", loginDto.Email);
                    return Ok(result.Data);
                }

                _logger.LogWarning("Login failed for {Email}: {Error}", loginDto.Email, result.Error);
                return Unauthorized(result.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
