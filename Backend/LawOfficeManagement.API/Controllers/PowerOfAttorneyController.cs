using LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.CreatePowerOfAttorney;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.DeletePowerOfAttorney;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.UpdatePowerOfAttorney;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetAllPowerOfAttorney;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetPowerOfAttorneyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize] // 🔒 اختياري – احذفها لو لم تفعل التوثيق
    public class PowerOfAttorneyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PowerOfAttorneyController> _logger;

        public PowerOfAttorneyController(IMediator mediator, ILogger<PowerOfAttorneyController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // 🟩 إنشاء وكالة جديدة
        [HttpPost("create")]
        public async Task<IActionResult> Create( CreatePowerOfAttorneyCommand command)
        {
            var id = await _mediator.Send(command);
            _logger.LogInformation("تم إنشاء وكالة جديدة بالمعرف {Id}", id);
            return Ok(new { Message = "تم إنشاء الوكالة بنجاح", Id = id });
        }

        // 🟨 تحديث وكالة
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdatePowerOfAttorneyCommand command)
        {
            if (id != command.Id)
                return BadRequest("معرف الوكالة غير متطابق.");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("الوكالة غير موجودة.");

            _logger.LogInformation("تم تحديث الوكالة رقم {Id}", id);
            return Ok(new { Message = "تم تحديث الوكالة بنجاح" });
        }

        // 🟥 حذف وكالة
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePowerOfAttorneyCommand(id));
            if (!result)
                return NotFound("الوكالة غير موجودة أو لم يتم حذفها.");

            _logger.LogInformation("تم حذف الوكالة رقم {Id}", id);
            return Ok(new { Message = "تم حذف الوكالة بنجاح" });
        }

        // 🟦 الحصول على جميع الوكالات
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllPowerOfAttorneyQuery());
            return Ok(list);
        }

        // 🟪 الحصول على وكالة محددة حسب المعرف
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _mediator.Send(new GetPowerOfAttorneyByIdQuery(id));
            if (dto == null)
                return NotFound("الوكالة غير موجودة.");

            return Ok(dto);
        }
    }
}
