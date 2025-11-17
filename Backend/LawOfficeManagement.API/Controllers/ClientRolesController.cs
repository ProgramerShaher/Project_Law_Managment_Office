using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Application.Features.ClientRoles.Commands.CreateClientRole;
using LawOfficeManagement.Application.Features.ClientRoles.Commands.DeleteClientRole;
using LawOfficeManagement.Application.Features.ClientRoles.Commands.UpdateClientRole;
using LawOfficeManagement.Application.Features.ClientRoles.Queries.GetAllClientRoles;
using LawOfficeManagement.Application.Features.ClientRoles.Queries.GetClientRoleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrator")]
    public class ClientRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClientRolesController> _logger;

        public ClientRolesController(IMediator mediator, ILogger<ClientRolesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientRoleDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllClientRolesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientRoleDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetClientRoleByIdQuery { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateClientRoleCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientRoleCommand command)
        {
            if (id != command.Id) return BadRequest();
            var ok = await _mediator.Send(command);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _mediator.Send(new DeleteClientRoleCommand { Id = id });
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
