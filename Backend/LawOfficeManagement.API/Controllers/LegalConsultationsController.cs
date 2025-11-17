// LawOfficeManagement.API.Controllers.LegalConsultationsController
using LawOfficeManagement.Application.Features.LegalConsultations.Commands;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Application.Features.LegalConsultations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalConsultationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LegalConsultationsController> _logger;

        public LegalConsultationsController(IMediator mediator, ILogger<LegalConsultationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على جميع الاستشارات القانونية
        /// </summary>
        /// <param name="includeInactive">يشمل المحذوفة</param>
        /// <returns>قائمة الاستشارات القانونية</returns>
        [HttpGet]
        public async Task<ActionResult<List<LegalConsultationDto>>> GetAll(bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على جميع الاستشارات القانونية - IncludeInactive: {IncludeInactive}", includeInactive);

                var query = new GetAllLegalConsultationsQuery { IncludeInactive = includeInactive };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب جميع الاستشارات القانونية");
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على استشارة قانونية بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الاستشارة</param>
        /// <returns>الاستشارة القانونية</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LegalConsultationDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على الاستشارة القانونية بالمعرف: {ConsultationId}", id);

                var query = new GetLegalConsultationByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الاستشارة القانونية غير موجودة: {ConsultationId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الاستشارة القانونية: {ConsultationId}", id);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على استشارات محامي معين
        /// </summary>
        /// <param name="lawyerId">معرف المحامي</param>
        /// <returns>قائمة استشارات المحامي</returns>
        [HttpGet("lawyer/{lawyerId}")]
        public async Task<ActionResult<List<LegalConsultationDto>>> GetByLawyer(int lawyerId)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على استشارات المحامي: {LawyerId}", lawyerId);

                var query = new GetLegalConsultationsByLawyerQuery { LawyerId = lawyerId };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب استشارات المحامي: {LawyerId}", lawyerId);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على استشارات بحالة معينة
        /// </summary>
        /// <param name="status">حالة الاستشارة</param>
        /// <returns>قائمة الاستشارات</returns>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<LegalConsultationDto>>> GetByStatus(string status)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على الاستشارات بحالة: {Status}", status);

                var query = new GetLegalConsultationsByStatusQuery { Status = status };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الاستشارات بحالة: {Status}", status);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// إنشاء استشارة قانونية جديدة
        /// </summary>
        /// <param name="createDto">بيانات الاستشارة الجديدة</param>
        /// <returns>معرف الاستشارة المنشأة</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateLegalConsultationDto createDto)
        {
            try
            {
                _logger.LogInformation("طلب إنشاء استشارة قانونية جديدة للعميل: {CustomerName}", createDto.CustomerName);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات إنشاء الاستشارة غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new CreateLegalConsultationCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "خطأ في بيانات إنشاء الاستشارة");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في العملية");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إنشاء الاستشارة القانونية");
                return StatusCode(500, "حدث خطأ أثناء إنشاء الاستشارة");
            }
        }

        /// <summary>
        /// تحديث استشارة قانونية
        /// </summary>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>نتيجة العملية</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLegalConsultationDto updateDto)
        {
            try
            {
                _logger.LogInformation("طلب تحديث الاستشارة القانونية: {ConsultationId}", updateDto.Id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات تحديث الاستشارة غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new UpdateLegalConsultationCommand { UpdateDto = updateDto };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الاستشارة غير موجودة للتحديث: {ConsultationId}", updateDto.Id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في تحديث الاستشارة: {ConsultationId}", updateDto.Id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث الاستشارة القانونية: {ConsultationId}", updateDto.Id);
                return StatusCode(500, "حدث خطأ أثناء التحديث");
            }
        }

        /// <summary>
        /// حذف استشارة قانونية
        /// </summary>
        /// <param name="id">معرف الاستشارة</param>
        /// <returns>نتيجة العملية</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("طلب حذف الاستشارة القانونية: {ConsultationId}", id);

                var command = new DeleteLegalConsultationCommand { Id = id };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الاستشارة غير موجودة للحذف: {ConsultationId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء حذف الاستشارة القانونية: {ConsultationId}", id);
                return StatusCode(500, "حدث خطأ أثناء الحذف");
            }
        }
    }
}