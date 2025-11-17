using LawOfficeManagement.Application.Features.CaseTypes.Commands;
using LawOfficeManagement.Application.Features.CaseTypes.Queries;
using LawOfficeManagement.Application.Features.CaseTypes.Queries.GetAllCaseTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/casetypes
        [HttpGet]
        public async Task<ActionResult<List<CaseTypeDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCaseTypesQuery());
            return Ok(result);
        }

        // GET: api/casetypes/with-categories


        //// GET: api/casetypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseTypeDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCaseTypeByIdQuery { Id = id });
            return Ok(result);
        }

        // POST: api/casetypes
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm]CreateCaseTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //// PUT: api/casetypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCaseTypeCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        //// DELETE: api/casetypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCaseTypeCommand { Id = id });
            return NoContent();
        }
    }
}