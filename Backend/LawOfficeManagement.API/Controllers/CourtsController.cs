using LawOfficeManagement.Application.Features.Courts.Commands.CreateCourt;
using LawOfficeManagement.Application.Features.Courts.Commands.DeleteCourt;
using LawOfficeManagement.Application.Features.Courts.Commands.UpdateCourt;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Application.Features.Courts.Queries.GetAllCourts;
using LawOfficeManagement.Application.Features.Courts.Queries.GetCourtById;
using LawOfficeManagement.Application.Features.Courts.Queries.GetCourtDivisionsByCourt;
using LawOfficeManagement.Application.Features.Courts.Queries.GetCourtsByType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourtsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourtsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourtDto>>> GetAll()
        {
            var list = await _mediator.Send(new GetAllCaseTypeQuery());
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CourtDto>> GetById(int id)
        {
            var item = await _mediator.Send(new GetCourtByIdQuery { Id = id });
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCaseTypeCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourtCommand command)
        {
            if (id != command.Id) return BadRequest();
            var ok = await _mediator.Send(command);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _mediator.Send(new DeleteCourtCommand { Id = id });
            if (!ok) return NotFound();
            return NoContent();
        }
        [HttpGet("by-type/{courtTypeId}")]
        public async Task<ActionResult> GetCourtsByType(int courtTypeId)
        {
            var query = new GetCourtsByTypeQuery { CourtTypeId = courtTypeId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{courtId}/divisions")]
        public async Task<ActionResult> GetCourtDivisions(int courtId)
        {
            var query = new GetCourtDivisionsByCourtQuery { CourtId = courtId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
