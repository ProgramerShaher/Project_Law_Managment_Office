using LawOfficeManagement.Application.Features.Cases.Commands.ActivateCaseStage;
using LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseStage;
using LawOfficeManagement.Application.Features.Cases.Commands.DeleteCaseStage;
using LawOfficeManagement.Application.Features.Cases.Commands.UpdateCaseStage;
using LawOfficeManagement.Application.Features.Cases.Queries.GetActiveCaseStage;
using LawOfficeManagement.Application.Features.Cases.Queries.GetCaseStageById;
using LawOfficeManagement.Application.Features.Cases.Queries.GetCaseStagesByCaseId;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/cases/{caseId}/stages")]
    [ApiController]
    public class CaseStagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CaseStagesController> _logger;

        public CaseStagesController(IMediator mediator, ILogger<CaseStagesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// جلب جميع مراحل القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="includeInactive">تضمين المراحل غير النشطة</param>
        /// <returns>قائمة بمراحل القضية</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseStageListDto>>> GetCaseStages(
            [Required] int caseId,
            [FromQuery] bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("جلب مراحل القضية {CaseId} - تضمين غير النشطة: {IncludeInactive}",
                    caseId, includeInactive);

                var query = new GetCaseStagesByCaseIdQuery
                {
                    CaseId = caseId,
                    IncludeInactive = includeInactive
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب مراحل القضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب مراحل القضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب مراحل القضية");
            }
        }

        /// <summary>
        /// جلب مرحلة محددة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف المرحلة</param>
        /// <returns>تفاصيل المرحلة</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CaseStageDetailsDto>> GetCaseStageById(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("جلب المرحلة {StageId} للقضية {CaseId}", id, caseId);

                var query = new GetCaseStageByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                // التحقق من أن المرحلة تابعة للقضية المطلوبة
                if (result.CaseId != caseId)
                {
                    _logger.LogWarning("المرحلة {StageId} لا تنتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "المرحلة غير موجودة" });
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "المرحلة غير موجودة {StageId} للقضية {CaseId}", id, caseId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب المرحلة {StageId} للقضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب المرحلة");
            }
        }

        /// <summary>
        /// جلب المرحلة النشطة للقضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <returns>المرحلة النشطة</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CaseStageDetailsDto>> GetActiveCaseStage([Required] int caseId)
        {
            try
            {
                _logger.LogInformation("جلب المرحلة النشطة للقضية {CaseId}", caseId);

                var query = new GetActiveCaseStageQuery { CaseId = caseId };
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    _logger.LogWarning("لا توجد مرحلة نشطة للقضية {CaseId}", caseId);
                    return NotFound(new { message = "لا توجد مرحلة نشطة للقضية" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب المرحلة النشطة للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب المرحلة النشطة");
            }
        }

        /// <summary>
        /// إنشاء مرحلة جديدة للقضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="createDto">بيانات المرحلة الجديدة</param>
        /// <returns>معرف المرحلة المنشأة</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateCaseStage(
            [Required] int caseId,
            [FromBody] CreateCaseStageDto createDto)
        {
            try
            {
                _logger.LogInformation("بدء إنشاء مرحلة جديدة للقضية {CaseId}", caseId);

                // تعيين معرف القضية من الـ route
                createDto.CaseId = caseId;

                var command = new CreateCaseStageCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                _logger.LogInformation("تم إنشاء المرحلة بنجاح بالمعرف {StageId} للقضية {CaseId}", result, caseId);

                return CreatedAtAction(
                    nameof(GetCaseStageById),
                    new { caseId = caseId, id = result },
                    new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات إنشاء المرحلة للقضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إنشاء المرحلة للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء إنشاء المرحلة");
            }
        }

        /// <summary>
        /// تحديث مرحلة موجودة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف المرحلة</param>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCaseStage(
            [Required] int caseId,
            [Required] int id,
            [FromBody] UpdateCaseStageDto updateDto)
        {
            try
            {
                _logger.LogInformation("بدء تحديث المرحلة {StageId} للقضية {CaseId}", id, caseId);

                // التحقق من أن المرحلة تابعة للقضية
                var stageQuery = new GetCaseStageByIdQuery { Id = id };
                var stage = await _mediator.Send(stageQuery);

                if (stage.CaseId != caseId)
                {
                    _logger.LogWarning("المرحلة {StageId} لا تنتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "المرحلة غير موجودة" });
                }

                var command = new UpdateCaseStageCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم تحديث المرحلة {StageId} بنجاح للقضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "المرحلة غير موجودة للتحديث {StageId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في بيانات تحديث المرحلة {StageId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث المرحلة {StageId} للقضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء تحديث المرحلة");
            }
        }

        /// <summary>
        /// حذف مرحلة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف المرحلة</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCaseStage(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء حذف المرحلة {StageId} للقضية {CaseId}", id, caseId);

                // التحقق من أن المرحلة تابعة للقضية
                var stageQuery = new GetCaseStageByIdQuery { Id = id };
                var stage = await _mediator.Send(stageQuery);

                if (stage.CaseId != caseId)
                {
                    _logger.LogWarning("المرحلة {StageId} لا تنتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "المرحلة غير موجودة" });
                }

                var command = new DeleteCaseStageCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم حذف المرحلة {StageId} بنجاح للقضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "المرحلة غير موجودة للحذف {StageId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "لا يمكن حذف المرحلة {StageId}: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء حذف المرحلة {StageId} للقضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء حذف المرحلة");
            }
        }

        /// <summary>
        /// تفعيل مرحلة محددة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف المرحلة</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateCaseStage(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء تفعيل المرحلة {StageId} للقضية {CaseId}", id, caseId);

                // التحقق من أن المرحلة تابعة للقضية
                var stageQuery = new GetCaseStageByIdQuery { Id = id };
                var stage = await _mediator.Send(stageQuery);

                if (stage.CaseId != caseId)
                {
                    _logger.LogWarning("المرحلة {StageId} لا تنتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "المرحلة غير موجودة" });
                }

                var command = new ActivateCaseStageCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم تفعيل المرحلة {StageId} بنجاح للقضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "المرحلة غير موجودة للتفعيل {StageId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في تفعيل المرحلة {StageId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تفعيل المرحلة {StageId} للقضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء تفعيل المرحلة");
            }
        }

        /// <summary>
        /// إلغاء تفعيل مرحلة محددة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف المرحلة</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateCaseStage(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء إلغاء تفعيل المرحلة {StageId} للقضية {CaseId}", id, caseId);

                // التحقق من أن المرحلة تابعة للقضية
                var stageQuery = new GetCaseStageByIdQuery { Id = id };
                var stage = await _mediator.Send(stageQuery);

                if (stage.CaseId != caseId)
                {
                    _logger.LogWarning("المرحلة {StageId} لا تنتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "المرحلة غير موجودة" });
                }

                var updateDto = new UpdateCaseStageDto
                {
                    Stage = stage.Stage,
                    Priority = stage.Priority,
                    IsActive = false,
                    EndDateStage = stage.EndDateStage
                };

                var command = new UpdateCaseStageCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم إلغاء تفعيل المرحلة {StageId} بنجاح للقضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجودة"))
                {
                    _logger.LogWarning(ex, "المرحلة غير موجودة لإلغاء التفعيل {StageId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في إلغاء تفعيل المرحلة {StageId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إلغاء تفعيل المرحلة {StageId} للقضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء إلغاء تفعيل المرحلة");
            }
        }
    }
}