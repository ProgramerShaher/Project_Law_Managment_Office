using LawOfficeManagement.Application.Features.CaseTeams.Commands.AssignLawyerToCase;
using LawOfficeManagement.Application.Features.CaseTeams.Commands.CreateCaseTeam;
using LawOfficeManagement.Application.Features.CaseTeams.Commands.DeleteCaseTeam;
using LawOfficeManagement.Application.Features.CaseTeams.Commands.UpdateCaseTeam;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetActiveCaseTeamMembers;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByCaseId;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamById;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByLawyerId;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamMembersWithDetails;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetLawyersAvailableForCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/cases/{caseId}/team")]
    [ApiController]
    public class CaseTeamsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CaseTeamsController> _logger;

        public CaseTeamsController(IMediator mediator, ILogger<CaseTeamsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// جلب جميع أعضاء فريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="includeInactive">تضمين الأعضاء غير النشطين</param>
        /// <returns>قائمة بأعضاء فريق القضية</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseTeamListDto>>> GetCaseTeam(
            [Required] int caseId,
            [FromQuery] bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("جلب فريق عمل القضية {CaseId} - تضمين غير النشطين: {IncludeInactive}",
                    caseId, includeInactive);

                var query = new GetCaseTeamByCaseIdQuery
                {
                    CaseId = caseId,
                    IncludeInactive = includeInactive
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب فريق القضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب فريق القضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب فريق القضية");
            }
        }

        /// <summary>
        /// جلب تفاصيل عضو فريق محدد
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف العضو في الفريق</param>
        /// <returns>تفاصيل العضو في الفريق</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CaseTeamDetailsDto>> GetCaseTeamMember(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("جلب تفاصيل العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);

                var query = new GetCaseTeamByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                // التحقق من أن العضو ينتمي للقضية المطلوبة
                if (result.CaseId != caseId)
                {
                    _logger.LogWarning("العضو {TeamMemberId} لا ينتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "العضو غير موجود في فريق القضية" });
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "العضو غير موجود {TeamMemberId} في فريق القضية {CaseId}", id, caseId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب العضو");
            }
        }

        /// <summary>
        /// جلب الأعضاء النشطين فقط لفريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <returns>قائمة بالأعضاء النشطين</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseTeamListDto>>> GetActiveCaseTeamMembers([Required] int caseId)
        {
            try
            {
                _logger.LogInformation("جلب الأعضاء النشطين لفريق القضية {CaseId}", caseId);

                var query = new GetActiveCaseTeamMembersQuery { CaseId = caseId };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب الأعضاء النشطين للقضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الأعضاء النشطين للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب الأعضاء النشطين");
            }
        }

        /// <summary>
        /// جلب أعضاء فريق القضية مع تفاصيل إضافية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="includeInactive">تضمين الأعضاء غير النشطين</param>
        /// <returns>قائمة مفصلة بأعضاء الفريق</returns>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseTeamMemberDetailsDto>>> GetCaseTeamMembersWithDetails(
            [Required] int caseId,
            [FromQuery] bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("جلب تفاصيل أعضاء فريق القضية {CaseId}", caseId);

                var query = new GetCaseTeamMembersWithDetailsQuery
                {
                    CaseId = caseId,
                    IncludeInactive = includeInactive
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب تفاصيل فريق القضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب تفاصيل فريق القضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب تفاصيل الفريق");
            }
        }

        /// <summary>
        /// جلب المحامين المتاحين للإضافة إلى القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <returns>قائمة بالمحامين المتاحين</returns>
        [HttpGet("available-lawyers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AvailableLawyerDto>>> GetAvailableLawyers([Required] int caseId)
        {
            try
            {
                _logger.LogInformation("جلب المحامين المتاحين للقضية {CaseId}", caseId);

                var query = new GetLawyersAvailableForCaseQuery { CaseId = caseId };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب المحامين المتاحين للقضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب المحامين المتاحين للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء جلب المحامين المتاحين");
            }
        }

        /// <summary>
        /// إضافة محامي جديد لفريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="createDto">بيانات العضو الجديد</param>
        /// <returns>معرف العضو المضاف</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddCaseTeamMember(
            [Required] int caseId,
            [FromBody] CreateCaseTeamDto createDto)
        {
            try
            {
                _logger.LogInformation("بدء إضافة محامي جديد لفريق القضية {CaseId}", caseId);

                // تعيين معرف القضية من الـ route
                createDto.CaseId = caseId;

                var command = new CreateCaseTeamCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                _logger.LogInformation("تم إضافة المحامي بنجاح بالمعرف {TeamMemberId} لفريق القضية {CaseId}",
                    result, caseId);

                return CreatedAtAction(
                    nameof(GetCaseTeamMember),
                    new { caseId = caseId, id = result },
                    new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات إضافة المحامي للقضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إضافة المحامي للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء إضافة المحامي");
            }
        }

        /// <summary>
        /// تعيين محامي للقضية (طريقة مبسطة)
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="assignDto">بيانات التعيين</param>
        /// <returns>معرف العضو المضاف</returns>
        [HttpPost("assign")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AssignLawyerToCase(
            [Required] int caseId,
            [FromBody] AssignLawyerToCaseCommand assignDto)
        {
            try
            {
                _logger.LogInformation("بدء تعيين المحامي {LawyerId} للقضية {CaseId}",
                    assignDto.LawyerId, caseId);

                // تعيين معرف القضية من الـ route
                assignDto.CaseId = caseId;

                var result = await _mediator.Send(assignDto);

                _logger.LogInformation("تم تعيين المحامي بنجاح بالمعرف {TeamMemberId} للقضية {CaseId}",
                    result, caseId);

                return CreatedAtAction(
                    nameof(GetCaseTeamMember),
                    new { caseId = caseId, id = result },
                    new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات تعيين المحامي للقضية {CaseId}", caseId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تعيين المحامي للقضية {CaseId}", caseId);
                return StatusCode(500, "حدث خطأ أثناء تعيين المحامي");
            }
        }

        /// <summary>
        /// تحديث بيانات عضو في فريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف العضو في الفريق</param>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCaseTeamMember(
            [Required] int caseId,
            [Required] int id,
            [FromBody] UpdateCaseTeamDto updateDto)
        {
            try
            {
                _logger.LogInformation("بدء تحديث العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);

                // التحقق من أن العضو ينتمي للقضية
                var memberQuery = new GetCaseTeamByIdQuery { Id = id };
                var member = await _mediator.Send(memberQuery);

                if (member.CaseId != caseId)
                {
                    _logger.LogWarning("العضو {TeamMemberId} لا ينتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "العضو غير موجود في فريق القضية" });
                }

                var command = new UpdateCaseTeamCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم تحديث العضو {TeamMemberId} بنجاح في فريق القضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجود"))
                {
                    _logger.LogWarning(ex, "العضو غير موجود للتحديث {TeamMemberId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في بيانات تحديث العضو {TeamMemberId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء تحديث العضو");
            }
        }

        /// <summary>
        /// حذف عضو من فريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف العضو في الفريق</param>
        /// <returns>لا يوجد محتوى</returns>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> RemoveCaseTeamMember(
        //    [Required] int caseId,
        //    [Required] int id)
        //{
        //    try
        //    {
        //        _logger.LogInformation("بدء حذف العضو {TeamMemberId} من فريق القضية {CaseId}", id, caseId);

        //        // التحقق من أن العضو ينتمي للقضية
        //        var memberQuery = new GetCaseTeamByIdQuery { Id = id };
        //        var member = await _mediator.Send(memberQuery);

        //        if (member.CaseId != caseId)
        //        {
        //            _logger.LogWarning("العضو {TeamMemberId} لا ينتمي للقضية {CaseId}", id, caseId);
        //            return NotFound(new { message = "العضو غير موجود في فريق القضية" });
        //        }

        //        var command = new DeleteCaseTeamCommand { Id = id };
        //        await _mediator.Send(command);

        //        _logger.LogInformation("تم حذف العضو {TeamMemberId} بنجاح من فريق القضية {CaseId}", id, caseId);

        //        return NoContent();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        if (ex.Message.Contains("غير موجود"))
        //        {
        //            _logger.LogWarning(ex, "العضو غير موجود للحذف {TeamMemberId}", id);
        //            return NotFound(new { message = ex.Message });
        //        }

        //        _logger.LogWarning(ex, "لا يمكن حذف العضو {TeamMemberId}: {Message}", id, ex.Message);
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "خطأ أثناء حذف العضو {TeamMemberId} من فريق القضية {CaseId}", id, caseId);
        //        return StatusCode(500, "حدث خطأ أثناء حذف العضو");
        //    }
        //}

        /// <summary>
        /// تفعيل عضو في فريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف العضو في الفريق</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateTeamMember(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء تفعيل العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);

                // التحقق من أن العضو ينتمي للقضية
                var memberQuery = new GetCaseTeamByIdQuery { Id = id };
                var member = await _mediator.Send(memberQuery);

                if (member.CaseId != caseId)
                {
                    _logger.LogWarning("العضو {TeamMemberId} لا ينتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "العضو غير موجود في فريق القضية" });
                }

                var updateDto = new UpdateCaseTeamDto
                {
                    Role = member.Role,
                    StartDate = member.StartDate,
                    EndDate = member.EndDate,
                    IsActive = true,
                };
                var command = new UpdateCaseTeamCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم تفعيل العضو {TeamMemberId} بنجاح في فريق القضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجود"))
                {
                    _logger.LogWarning(ex, "العضو غير موجود للتفعيل {TeamMemberId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في تفعيل العضو {TeamMemberId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تفعيل العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء تفعيل العضو");
            }
        }

        /// <summary>
        /// إلغاء تفعيل عضو في فريق القضية
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        /// <param name="id">معرف العضو في الفريق</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateTeamMember(
            [Required] int caseId,
            [Required] int id)
        {
            try
            {
                _logger.LogInformation("بدء إلغاء تفعيل العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);

                // التحقق من أن العضو ينتمي للقضية
                var memberQuery = new GetCaseTeamByIdQuery { Id = id };
                var member = await _mediator.Send(memberQuery);

                if (member.CaseId != caseId)
                {
                    _logger.LogWarning("العضو {TeamMemberId} لا ينتمي للقضية {CaseId}", id, caseId);
                    return NotFound(new { message = "العضو غير موجود في فريق القضية" });
                }

                var updateDto = new UpdateCaseTeamDto
                {
                    Role = member.Role,
                    StartDate = member.StartDate,
                    EndDate = DateTime.UtcNow, // تعيين تاريخ انتهاء
                    IsActive = false,
                };

                var command = new UpdateCaseTeamCommand { Id = id, UpdateDto = updateDto };
                await _mediator.Send(command);

                _logger.LogInformation("تم إلغاء تفعيل العضو {TeamMemberId} بنجاح في فريق القضية {CaseId}", id, caseId);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("غير موجود"))
                {
                    _logger.LogWarning(ex, "العضو غير موجود لإلغاء التفعيل {TeamMemberId}", id);
                    return NotFound(new { message = ex.Message });
                }

                _logger.LogWarning(ex, "خطأ في إلغاء تفعيل العضو {TeamMemberId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إلغاء تفعيل العضو {TeamMemberId} في فريق القضية {CaseId}", id, caseId);
                return StatusCode(500, "حدث خطأ أثناء إلغاء تفعيل العضو");
            }
        }

        /// <summary>
        /// جلب قضايا محامي معين
        /// </summary>
        /// <param name="lawyerId">معرف المحامي</param>
        /// <param name="includeInactive">تضمين القضايا غير النشطة</param>
        /// <returns>قائمة بقضايا المحامي</returns>
        [HttpGet("lawyer/{lawyerId}/cases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CaseTeamLawyerDto>>> GetLawyerCases(
            [Required] int lawyerId,
            [FromQuery] bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("جلب قضايا المحامي {LawyerId} - تضمين غير النشطة: {IncludeInactive}",
                    lawyerId, includeInactive);

                var query = new GetCaseTeamByLawyerIdQuery
                {
                    LawyerId = lawyerId,
                    IncludeInactive = includeInactive
                };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات جلب قضايا المحامي {LawyerId}", lawyerId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب قضايا المحامي {LawyerId}", lawyerId);
                return StatusCode(500, "حدث خطأ أثناء جلب قضايا المحامي");
            }
        }
    }
}