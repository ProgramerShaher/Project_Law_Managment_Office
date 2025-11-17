// LawOfficeManagement.API.Controllers.OpponentsController
using LawOfficeManagement.Application.Features.Opponents.Commands;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Application.Features.Opponents.Queries;
using LawOfficeManagement.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpponentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OpponentsController> _logger;

        public OpponentsController(IMediator mediator, ILogger<OpponentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على جميع الخصوم
        /// </summary>
        /// <param name="includeInactive">يشمل المحذوفة</param>
        /// <returns>قائمة الخصوم</returns>
        [HttpGet]
        public async Task<ActionResult<List<OpponentDto>>> GetAll(bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على جميع الخصوم - IncludeInactive: {IncludeInactive}", includeInactive);

                var query = new GetAllOpponentsQuery { IncludeInactive = includeInactive };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب جميع الخصوم");
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على خصم بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <returns>الخصم</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OpponentDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على الخصم بالمعرف: {OpponentId}", id);

                var query = new GetOpponentByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخصم غير موجود: {OpponentId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الخصم: {OpponentId}", id);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على خصم مع قضاياه
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <returns>الخصم مع قضاياه</returns>
        [HttpGet("{id}/with-cases")]
        public async Task<ActionResult<OpponentCaseDto>> GetWithCases(int id)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على الخصم مع قضاياه: {OpponentId}", id);

                var query = new GetOpponentWithCasesQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخصم غير موجود: {OpponentId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الخصم مع قضاياه: {OpponentId}", id);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على الخصوم بنوع معين
        /// </summary>
        /// <param name="type">نوع الخصم</param>
        /// <returns>قائمة الخصوم</returns>
        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<OpponentDto>>> GetByType(OpponentType type)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على الخصوم بنوع: {OpponentType}", type);

                var query = new GetOpponentsByTypeQuery { Type = type };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب الخصوم بنوع: {OpponentType}", type);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// بحث في الخصوم
        /// </summary>
        /// <param name="searchTerm">مصطلح البحث</param>
        /// <returns>قائمة الخصوم</returns>
        [HttpGet("search")]
        public async Task<ActionResult<List<OpponentDto>>> Search([FromQuery] string searchTerm)
        {
            try
            {
                _logger.LogInformation("طلب بحث في الخصوم بالمصطلح: {SearchTerm}", searchTerm);

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return Ok(new List<OpponentDto>());
                }

                var query = new SearchOpponentsQuery { SearchTerm = searchTerm };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء البحث في الخصوم: {SearchTerm}", searchTerm);
                return StatusCode(500, "حدث خطأ أثناء البحث");
            }
        }

        /// <summary>
        /// إنشاء خصم جديد
        /// </summary>
        /// <param name="createDto">بيانات الخصم الجديد</param>
        /// <returns>معرف الخصم المنشأ</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOpponentDto createDto)
        {
            try
            {
                _logger.LogInformation("طلب إنشاء خصم جديد: {OpponentName}", createDto.OpponentName);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات إنشاء الخصم غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new CreateOpponentCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في إنشاء الخصم: {OpponentName}", createDto.OpponentName);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إنشاء الخصم");
                return StatusCode(500, "حدث خطأ أثناء إنشاء الخصم");
            }
        }

        /// <summary>
        /// تحديث خصم
        /// </summary>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>نتيجة العملية</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOpponentDto updateDto)
        {
            try
            {
                _logger.LogInformation("طلب تحديث الخصم: {OpponentId}", updateDto.Id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات تحديث الخصم غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new UpdateOpponentCommand { UpdateDto = updateDto };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخصم غير موجود للتحديث: {OpponentId}", updateDto.Id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في تحديث الخصم: {OpponentId}", updateDto.Id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث الخصم: {OpponentId}", updateDto.Id);
                return StatusCode(500, "حدث خطأ أثناء التحديث");
            }
        }

        /// <summary>
        /// حذف خصم
        /// </summary>
        /// <param name="id">معرف الخصم</param>
        /// <returns>نتيجة العملية</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("طلب حذف الخصم: {OpponentId}", id);

                var command = new DeleteOpponentCommand { Id = id };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخصم غير موجود للحذف: {OpponentId}", id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "لا يمكن حذف الخصم: {OpponentId}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء حذف الخصم: {OpponentId}", id);
                return StatusCode(500, "حدث خطأ أثناء الحذف");
            }
        }
    }
}