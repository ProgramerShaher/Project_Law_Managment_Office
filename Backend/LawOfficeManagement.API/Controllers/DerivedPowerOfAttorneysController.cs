using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.CreateDerivedPowerOfAttorney;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.DeleteDerivedPowerOfAttorney;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.UpdateDerivedPowerOfAttorney;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetAllDerivedPowerOfAttorneys;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetDerivedPowerOfAttorneyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerivedPowerOfAttorneysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DerivedPowerOfAttorneysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DerivedPowerOfAttorneyDto>>> GetAll()
        {
            var query = new GetAllDerivedPowerOfAttorneysQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DerivedPowerOfAttorneyDto>> GetById(int id)
        {
            var query = new GetDerivedPowerOfAttorneyByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateDerivedPowerOfAttorneyDto createDto)
        {
            var command = new CreateDerivedPowerOfAttorneyCommand { CreateDto = createDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateDerivedPowerOfAttorneyDto updateDto)
        {
            var command = new UpdateDerivedPowerOfAttorneyCommand { Id = id, UpdateDto = updateDto };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDerivedPowerOfAttorneyCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}