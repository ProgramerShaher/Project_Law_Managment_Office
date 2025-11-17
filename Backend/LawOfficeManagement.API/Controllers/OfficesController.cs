using LawOfficeManagement.Application.Features.Offices.Commands.Add;
using LawOfficeManagement.Application.Features.Offices.Commands.Update;
using LawOfficeManagement.Application.Features.Offices.Queries;
using LawOfficeManagement.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // يمكن إزالتها مؤقتًا أثناء التطوير
    public class OfficesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OfficesController> _logger;

        public OfficesController(IMediator mediator, ILogger<OfficesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// إنشاء مكتب جديد (يسمح بوجود مكتب واحد فقط في النظام)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOffice([FromBody] AddOfficeCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("تم استقبال طلب إضافة مكتب جديد");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var officeId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetOffice), new { id = officeId }, new { id = officeId });
        }

        /// <summary>
        /// return BadRequest(ModelState);
        /// تعديل بيانات المكتب الحالي
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateOffice( UpdateOfficeCommand command, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(command);
            return NoContent();

        }

        /// <summary>
        /// الحصول على بيانات المكتب الحالي (اختياري)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOffice(CancellationToken cancellationToken)
        {
            _logger.LogInformation("طلب جلب بيانات المكتب");
            var office = await _mediator.Send(new GetOfficeQuery(), cancellationToken);
            return Ok(office);
        }
    }
}
