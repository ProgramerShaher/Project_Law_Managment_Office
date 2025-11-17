using LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer;
using LawOfficeManagement.Application.Features.Lawyers.Commands.UpdateLawyer;
using LawOfficeManagement.Application.Features.Lawyers.Commands.DeleteLawyer;
using LawOfficeManagement.Application.Features.Lawyers.Queries.GetLawyerById;
using LawOfficeManagement.Application.Features.Lawyers.Queries.GetAllLawyers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // يمكنك تفعيلها لاحقًا لتأمين كل النقاط
    public class LawyersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LawyersController> _logger;

        public LawyersController(IMediator mediator, ILogger<LawyersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// جلب جميع المحامين (غير المحذوفين).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllLawyers()
        {
            var result = await _mediator.Send(new GetAllLawyersQuery());
            return Ok(result);
        }

        /// <summary>
        /// إنشاء محامي جديد.
        /// </summary>
        [HttpPost]
        // [Authorize(Roles = "Administrator")] // مثال على تقييد الوصول
        public async Task<IActionResult> CreateLawyer( CreateLawyerCommand command)
        {
            _logger.LogInformation("بدء إنشاء محامي جديد.");

            var lawyerId = await _mediator.Send(command);

            _logger.LogInformation("تم إنشاء المحامي بنجاح بالمعرف: {LawyerId}", lawyerId);

            return CreatedAtAction(nameof(GetLawyerById), new { id = lawyerId }, new { LawyerId = lawyerId });
        }

        /// <summary>
        /// جلب بيانات محامي عبر المعرف.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLawyerById(int id)
        {
            var result = await _mediator.Send(new GetLawyerByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// تحديث بيانات محامي موجود.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLawyer(int id, [FromForm] UpdateLawyerCommand command)
        {
            if (id != command.Id)
                return BadRequest("رقم المعرف في الرابط لا يطابق المعرف في البيانات.");

            var ok = await _mediator.Send(command);
            if (!ok)
                return NotFound();

            _logger.LogInformation("تم تحديث بيانات المحامي بالمعرف {LawyerId}", id);
            return NoContent();
        }

        /// <summary>
        /// حذف محامي (حذف منطقي).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLawyer(int id)
        {
            var ok = await _mediator.Send(new DeleteLawyerCommand { Id = id });
            if (!ok)
                return NotFound();

            _logger.LogInformation("تم حذف المحامي بالمعرف {LawyerId}", id);
            return NoContent();
        }
    }
}
