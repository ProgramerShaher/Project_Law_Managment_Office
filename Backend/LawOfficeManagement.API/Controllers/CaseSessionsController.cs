using System.ComponentModel.DataAnnotations;
using LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseSession;
using LawOfficeManagement.Application.Features.Cases.Commands.Dtos;
using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Queries;
using LawOfficeManagement.Application.Features.CaseSessions.Queries.Queries;
using LawOfficeManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.WebAPI.Controllers
{
    /// <summary>
    /// واجهات برمجة التطبيقات (APIs) لإدارة جلسات القضايا
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CaseSessionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseSessionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region 🔍 عمليات القراءة (GET)

        /// <summary>
        /// 📋 الحصول على جميع الجلسات - بيانات أساسية
        /// </summary>
        /// <param name="caseId">معرف القضية (اختياري)</param>
        /// <param name="courtId">معرف المحكمة (اختياري)</param>
        /// <param name="fromDate">تاريخ البدء (اختياري)</param>
        /// <param name="toDate">تاريخ الانتهاء (اختياري)</param>
        /// <param name="status">حالة الجلسة (اختياري)</param>
        /// <param name="lawyerId">معرف المحامي (اختياري)</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<CaseSessionDto>), 200)]
        public async Task<ActionResult<List<CaseSessionDto>>> GetAll(
            [FromQuery] int? caseId = null,
            [FromQuery] int? courtId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] CaseSessionStatus? status = null,
            [FromQuery] int? lawyerId = null)
        {
            var query = new GetAllCaseSessionsQuery
            {
                CaseId = caseId,
                CourtId = courtId,
                FromDate = fromDate,
                ToDate = toDate,
                Status = status,
                LawyerId = lawyerId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// 🔍 الحصول على جميع الجلسات - تفاصيل كاملة
        /// </summary>
        /// <param name="caseId">معرف القضية (اختياري)</param>
        /// <param name="courtId">معرف المحكمة (اختياري)</param>
        /// <param name="fromDate">تاريخ البدء (اختياري)</param>
        /// <param name="toDate">تاريخ الانتهاء (اختياري)</param>
        /// <param name="status">حالة الجلسة (اختياري)</param>
        /// <param name="lawyerId">معرف المحامي (اختياري)</param>
        /// <param name="includeEvidences">تضمين الأدلة (افتراضي: true)</param>
        /// <param name="includeWitnesses">تضمين الشهود (افتراضي: true)</param>
        [HttpGet("with-details")]
        [ProducesResponseType(typeof(List<CaseSessionWithDetailsDto>), 200)]
        public async Task<ActionResult<List<CaseSessionWithDetailsDto>>> GetAllWithDetails(
            [FromQuery] int? caseId = null,
            [FromQuery] int? courtId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] CaseSessionStatus? status = null,
            [FromQuery] int? lawyerId = null,
            [FromQuery] bool includeEvidences = true,
            [FromQuery] bool includeWitnesses = true)
        {
            var query = new GetAllCaseSessionsWithDetailsQuery
            {
                CaseId = caseId,
                CourtId = courtId,
                FromDate = fromDate,
                ToDate = toDate,
                Status = status,
                LawyerId = lawyerId,
                IncludeEvidences = includeEvidences,
                IncludeWitnesses = includeWitnesses
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// 📄 الحصول على جلسة محددة - بيانات أساسية
        /// </summary>
        /// <param name="id">معرف الجلسة</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CaseSessionDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CaseSessionDto>> GetById(int id)
        {
            var query = new GetCaseSessionByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// 📄 الحصول على جلسة محددة - تفاصيل كاملة
        /// </summary>
        /// <param name="id">معرف الجلسة</param>
        [HttpGet("with-details/{id}")]
        [ProducesResponseType(typeof(CaseSessionWithDetailsDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CaseSessionWithDetailsDto>> GetByIdWithDetails(int id)
        {
            var query = new GetCaseSessionByIdWithDetailsQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// 📂 الحصول على جلسات قضية محددة
        /// </summary>
        /// <param name="caseId">معرف القضية</param>
        [HttpGet("case/{caseId}")]
        [ProducesResponseType(typeof(List<CaseSessionDto>), 200)]
        public async Task<ActionResult<List<CaseSessionDto>>> GetByCaseId(int caseId)
        {
            var query = new GetCaseSessionsByCaseIdQuery { CaseId = caseId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #endregion

        #region ➕ عمليات الإنشاء (POST)

        /// <summary>
        /// ➕ إنشاء جلسة جديدة
        /// </summary>
        /// <param name="createDto">بيانات إنشاء الجلسة الجديدة</param>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCaseSessionDto createDto)
        {
            var command = new CreateCaseSessionCommand { CreateCaseSessionDto = createDto };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        #endregion

        #region ✏️ عمليات التحديث (PUT)

        /// <summary>
        /// ✏️ تحديث الجلسة - بيانات أساسية
        /// </summary>
        /// <param name="id">معرف الجلسة المراد تحديثها</param>
        /// <param name="updateDto">بيانات التحديث الأساسية</param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCaseSessionDto updateDto)
        {
            var command = new UpdateCaseSessionCommand { Id = id, UpdateCaseSessionDto = updateDto };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// ✏️ تحديث الجلسة - تفاصيل كاملة
        /// </summary>
        /// <param name="id">معرف الجلسة المراد تحديثها</param>
        /// <param name="request">بيانات التحديث الكاملة</param>
        [HttpPut("{id}/details")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateWithDetails(int id, [FromBody] UpdateCaseSessionWithDetailsRequest request)
        {
            var command = new UpdateCaseSessionWithDetailsCommand
            {
                Id = id,
                UpdateCaseSessionDto = request.UpdateCaseSessionDto,
                Evidences = request.Evidences,
                Witnesses = request.Witnesses
            };
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region 🗑️ عمليات الحذف (DELETE)

        /// <summary>
        /// 🗑️ حذف الجلسة
        /// </summary>
        /// <param name="id">معرف الجلسة المراد حذفها</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCaseSessionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region 🔄 عمليات التحديث الجزئي (PATCH)

        /// <summary>
        /// 🔄 تحديث حالة الجلسة
        /// </summary>
        /// <param name="id">معرف الجلسة</param>
        /// <param name="statusDto">بيانات حالة الجلسة الجديدة</param>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateSessionStatusDto statusDto)
        {
            var command = new UpdateCaseSessionStatusCommand
            {
                Id = id,
                SessionStatus = statusDto.SessionStatus
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// 🔄 تحديث الحضور
        /// </summary>
        /// <param name="id">معرف الجلسة</param>
        /// <param name="attendanceDto">بيانات الحضور</param>
        [HttpPatch("{id}/attendance")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAttendance(int id, [FromBody] UpdateAttendanceDto attendanceDto)
        {
            var command = new UpdateCaseSessionAttendanceCommand
            {
                Id = id,
                LawyerAttended = attendanceDto.LawyerAttended,
                ClientAttended = attendanceDto.ClientAttended
            };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// 🔄 تحديث القرار
        /// </summary>
        /// <param name="id">معرف الجلسة</param>
        /// <param name="decisionDto">بيانات القرار والجلسة التالية</param>
        [HttpPatch("{id}/decision")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDecision(int id, [FromBody] UpdateDecisionDto decisionDto)
        {
            var command = new UpdateCaseSessionDecisionCommand
            {
                Id = id,
                Decision = decisionDto.Decision,
                NextSessionDate = decisionDto.NextSessionDate
            };
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }

    #region 📦 نماذج البيانات (DTOs)

    /// <summary>
    /// نموذج طلب تحديث الجلسة مع التفاصيل
    /// </summary>
    public class UpdateCaseSessionWithDetailsRequest
    {
        /// <summary>
        /// بيانات تحديث الجلسة الأساسية
        /// </summary>
        [Required]
        public UpdateCaseSessionDto UpdateCaseSessionDto { get; set; } = new();

        /// <summary>
        /// قائمة الأدلة المحدثة
        /// </summary>
        public List<UpdateCaseEvidenceDto> Evidences { get; set; } = new();

        /// <summary>
        /// قائمة الشهود المحدثين
        /// </summary>
        public List<UpdateCaseWitnessDto> Witnesses { get; set; } = new();
    }

    /// <summary>
    /// نموذج تحديث حالة الجلسة
    /// </summary>
    public class UpdateSessionStatusDto
    {
        /// <summary>
        /// الحالة الجديدة للجلسة
        /// </summary>
        [Required]
        public CaseSessionStatus SessionStatus { get; set; }
    }

    /// <summary>
    /// نموذج تحديث الحضور
    /// </summary>
    public class UpdateAttendanceDto
    {
        /// <summary>
        /// حضور المحامي
        /// </summary>
        [Required]
        public bool LawyerAttended { get; set; }

        /// <summary>
        /// حضور العميل
        /// </summary>
        [Required]
        public bool ClientAttended { get; set; }
    }

    /// <summary>
    /// نموذج تحديث القرار
    /// </summary>
    public class UpdateDecisionDto
    {
        /// <summary>
        /// القرار الصادر في الجلسة
        /// </summary>
        [StringLength(2000)]
        public string? Decision { get; set; }

        /// <summary>
        /// تاريخ الجلسة التالية
        /// </summary>
        public DateTime? NextSessionDate { get; set; }
    }

    #endregion
}