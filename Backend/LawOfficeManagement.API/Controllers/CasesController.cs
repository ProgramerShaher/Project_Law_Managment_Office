using LawOfficeManagement.Application.Features.Cases.Commands.ArchiveCase;
using LawOfficeManagement.Application.Features.Cases.Commands.CreateCase;
using LawOfficeManagement.Application.Features.Cases.Commands.DeleteCase;
using LawOfficeManagement.Application.Features.Cases.Commands.UnarchiveCase;
using LawOfficeManagement.Application.Features.Cases.Commands.UpdateCase;
using LawOfficeManagement.Application.Features.Cases.Dtos;
using LawOfficeManagement.Application.Features.Cases.Queries;
using LawOfficeManagement.Application.Features.Cases.Queries.GetAllCases;
using LawOfficeManagement.Application.Features.Cases.Queries.GetCaseById;
using LawOfficeManagement.Application.Features.Cases.Queries.GetCasesByClient;
using LawOfficeManagement.Core.Entities.Cases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CasesController> _logger;

        public CasesController(IMediator mediator, ILogger<CasesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// جلب جميع القضايا
        /// </summary>
        /// <param name="includeArchived">تضمين القضايا المؤرشفة</param>
        /// <returns>قائمة بالقضايا</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseListDto>>> GetAllCases([FromQuery] bool includeArchived = false)
        {
            try
            {
                _logger.LogInformation("جلب جميع القضايا - تضمين المؤرشفة: {IncludeArchived}", includeArchived);

                var query = new GetAllCasesQuery { IncludeArchived = includeArchived };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب جميع القضايا");
                return StatusCode(500, "حدث خطأ أثناء جلب القضايا");
            }
        }

        /// <summary>
        /// جلب قضية محددة بالمعرف
        /// </summary>
        /// <param name="id">معرف القضية</param>
        /// <returns>تفاصيل القضية</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CaseDetailsDto>> GetCaseById([Required] int id)
        {
            try
            {
                _logger.LogInformation("جلب القضية بالمعرف {CaseId}", id);

                var query = new GetCaseByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "القضية غير موجودة بالمعرف {CaseId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب القضية {CaseId}", id);
                return StatusCode(500, "حدث خطأ أثناء جلب القضية");
            }
        }

        /// <summary>
        /// إنشاء قضية جديدة
        /// </summary>
        /// <param name="createDto">بيانات القضية الجديدة</param>
        /// <returns>معرف القضية المنشأة</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateCase([FromBody] CreateCaseDto createDto)
        {
            try
            {
                _logger.LogInformation("بدء إنشاء قضية جديدة: {Title}", createDto.Title);

                var command = new CreateCaseCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                _logger.LogInformation("تم إنشاء القضية بنجاح بالمعرف {CaseId}", result);

                return CreatedAtAction(nameof(GetCaseById), new { id = result }, new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات إنشاء القضية");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إنشاء القضية");
                return StatusCode(500, "حدث خطأ أثناء إنشاء القضية");
            }
        }

        /// <summary>
        /// تحديث قضية موجودة
        /// </summary>
        /// <param name="id">معرف القضية</param>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCase([Required] int id, [FromBody] UpdateCaseDto updateDto)
        {
            try
            {
                _logger.LogInformation("بدء تحديث القضية {CaseId}", id);

                var command = new UpdateCaseCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم تحديث القضية {CaseId} بنجاح", id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "القضية غير موجودة للتحديث {CaseId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في بيانات تحديث القضية {CaseId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث القضية {CaseId}", id);
                return StatusCode(500, "حدث خطأ أثناء تحديث القضية");
            }
        }

        /// <summary>
        /// حذف قضية
        /// </summary>
        /// <param name="id">معرف القضية</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCase([Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء حذف القضية {CaseId}", id);

                var command = new DeleteCaseCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم حذف القضية {CaseId} بنجاح", id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "القضية غير موجودة للحذف {CaseId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "لا يمكن حذف القضية {CaseId}: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء حذف القضية {CaseId}", id);
                return StatusCode(500, "حدث خطأ أثناء حذف القضية");
            }
        }

        /// <summary>
        /// أرشفة قضية
        /// </summary>
        /// <param name="id">معرف القضية</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ArchiveCase([Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء أرشفة القضية {CaseId}", id);

                var command = new ArchiveCaseCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم أرشفة القضية {CaseId} بنجاح", id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "القضية غير موجودة للأرشفة {CaseId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء أرشفة القضية {CaseId}", id);
                return StatusCode(500, "حدث خطأ أثناء أرشفة القضية");
            }
        }

        /// <summary>
        /// إلغاء أرشفة قضية
        /// </summary>
        /// <param name="id">معرف القضية</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnarchiveCase([Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء إلغاء أرشفة القضية {CaseId}", id);

                var command = new UnarchiveCaseCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم إلغاء أرشفة القضية {CaseId} بنجاح", id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "القضية غير موجودة لإلغاء الأرشفة {CaseId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إلغاء أرشفة القضية {CaseId}", id);
                return StatusCode(500, "حدث خطأ أثناء إلغاء أرشفة القضية");
            }
        }

        /// <summary>
        /// جلب القضايا بحالة محددة
        /// </summary>
        /// <param name="status">حالة القضية</param>
        /// <param name="includeArchived">تضمين القضايا المؤرشفة</param>
        /// <returns>قائمة بالقضايا</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseListDto>>> GetCasesByStatus(
            [Required] CaseStatus status,
            [FromQuery] bool includeArchived = false)
        {
            try
            {
                _logger.LogInformation("جلب القضايا بحالة {Status}", status);

                var query = new GetCasesByStatusQuery
                {
                    Status = status,
                    IncludeArchived = includeArchived
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب القضايا بحالة {Status}", status);
                return StatusCode(500, "حدث خطأ أثناء جلب القضايا");
            }
        }

        /// <summary>
        /// جلب قضايا عميل محدد
        /// </summary>
        /// <param name="clientId">معرف العميل</param>
        /// <param name="includeArchived">تضمين القضايا المؤرشفة</param>
        /// <returns>قائمة بقضايا العميل</returns>
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseListDto>>> GetCasesByClient(
            [Required] int clientId,
            [FromQuery] bool includeArchived = false)
        {
            try
            {
                _logger.LogInformation("جلب قضايا العميل {ClientId}", clientId);

                var query = new GetCasesByClientQuery
                {
                    ClientId = clientId,
                    IncludeArchived = includeArchived
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب قضايا العميل {ClientId}", clientId);
                return StatusCode(500, "حدث خطأ أثناء جلب قضايا العميل");
            }
        }
    }
}