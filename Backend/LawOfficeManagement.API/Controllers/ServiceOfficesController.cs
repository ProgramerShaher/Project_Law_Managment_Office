// LawOfficeManagement.API.Controllers.ServiceOfficesController
using LawOfficeManagement.Application.Features.ServiceOffices.Commands;
using LawOfficeManagement.Application.Features.ServiceOffices.DTOs;
using LawOfficeManagement.Application.Features.ServiceOffices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceOfficesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ServiceOfficesController> _logger;

        public ServiceOfficesController(IMediator mediator, ILogger<ServiceOfficesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على جميع خدمات المكتب
        /// </summary>
        /// <param name="includeInactive">يشمل المحذوفة</param>
        /// <returns>قائمة خدمات المكتب</returns>
        [HttpGet]
        public async Task<ActionResult<List<ServiceOfficeDto>>> GetAll(bool includeInactive = false)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على جميع خدمات المكتب - IncludeInactive: {IncludeInactive}", includeInactive);

                var query = new GetAllServiceOfficesQuery { IncludeInactive = includeInactive };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب جميع خدمات المكتب");
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على خدمة مكتب بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف الخدمة</param>
        /// <returns>خدمة المكتب</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOfficeDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على خدمة المكتب بالمعرف: {ServiceId}", id);

                var query = new GetServiceOfficeByIdQuery { Id = id };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "خدمة المكتب غير موجودة: {ServiceId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب خدمة المكتب: {ServiceId}", id);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// الحصول على خدمات المكتب ضمن نطاق سعري
        /// </summary>
        /// <param name="minPrice">أقل سعر</param>
        /// <param name="maxPrice">أعلى سعر</param>
        /// <returns>قائمة خدمات المكتب</returns>
        [HttpGet("price-range")]
        public async Task<ActionResult<List<ServiceOfficeDto>>> GetByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            try
            {
                _logger.LogInformation("طلب الحصول على خدمات المكتب بالسعر من {MinPrice} إلى {MaxPrice}", minPrice, maxPrice);

                if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                {
                    _logger.LogWarning("نطاق السعر غير صالح: {MinPrice} - {MaxPrice}", minPrice, maxPrice);
                    return BadRequest("نطاق السعر غير صالح");
                }

                var query = new GetServiceOfficesByPriceRangeQuery
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };

                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء جلب خدمات المكتب بالنطاق السعري: {MinPrice}-{MaxPrice}", minPrice, maxPrice);
                return StatusCode(500, "حدث خطأ أثناء جلب البيانات");
            }
        }

        /// <summary>
        /// إنشاء خدمة مكتب جديدة
        /// </summary>
        /// <param name="createDto">بيانات الخدمة الجديدة</param>
        /// <returns>معرف الخدمة المنشأة</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateServiceOfficeDto createDto)
        {
            try
            {
                _logger.LogInformation("طلب إنشاء خدمة مكتب جديدة: {ServiceName}", createDto.ServiceName);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات إنشاء الخدمة غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new CreateServiceOfficeCommand { CreateDto = createDto };
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في إنشاء الخدمة: {ServiceName}", createDto.ServiceName);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء إنشاء خدمة المكتب");
                return StatusCode(500, "حدث خطأ أثناء إنشاء الخدمة");
            }
        }

        /// <summary>
        /// تحديث خدمة مكتب
        /// </summary>
        /// <param name="updateDto">بيانات التحديث</param>
        /// <returns>نتيجة العملية</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateServiceOfficeDto updateDto)
        {
            try
            {
                _logger.LogInformation("طلب تحديث خدمة المكتب: {ServiceId}", updateDto.Id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات تحديث الخدمة غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new UpdateServiceOfficeCommand { UpdateDto = updateDto };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخدمة غير موجودة للتحديث: {ServiceId}", updateDto.Id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "مشكلة في تحديث الخدمة: {ServiceId}", updateDto.Id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء تحديث خدمة المكتب: {ServiceId}", updateDto.Id);
                return StatusCode(500, "حدث خطأ أثناء التحديث");
            }
        }

        /// <summary>
        /// حذف خدمة مكتب
        /// </summary>
        /// <param name="id">معرف الخدمة</param>
        /// <returns>نتيجة العملية</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("طلب حذف خدمة المكتب: {ServiceId}", id);

                var command = new DeleteServiceOfficeCommand { Id = id };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "الخدمة غير موجودة للحذف: {ServiceId}", id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "لا يمكن حذف الخدمة: {ServiceId}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ أثناء حذف خدمة المكتب: {ServiceId}", id);
                return StatusCode(500, "حدث خطأ أثناء الحذف");
            }
        }
    }
}