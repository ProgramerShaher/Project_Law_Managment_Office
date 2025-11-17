using LawOfficeManagement.Application.Features.CourtTypes.Commands.CreateCourtType;
using LawOfficeManagement.Application.Features.CourtTypes.Commands.DeleteCourtType;
using LawOfficeManagement.Application.Features.CourtTypes.Commands.UpdateCourtType;
using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using LawOfficeManagement.Application.Features.CourtTypes.Queries.GetAllCourtTypes;
using LawOfficeManagement.Application.Features.CourtTypes.Queries.GetCourtTypeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourtTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourtTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourtTypeDto>>> GetAll()
        {
            var list = await _mediator.Send(new GetAllCourtTypesQuery());
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourtTypeDto>> GetById(int id)
        {
            var item = await _mediator.Send(new GetCourtTypeByIdQuery { Id = id });
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCourtTypeCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourtTypeCommand command)
        {
            if (id != command.Id) return BadRequest();
            var ok = await _mediator.Send(command);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _mediator.Send(new DeleteCourtTypeCommand { Id = id });
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
