using LawOfficeManagement.Application.Features.Contracts.Commands.CreateContract;
using LawOfficeManagement.Application.Features.Contracts.Commands.DeleteContract;
using LawOfficeManagement.Application.Features.Contracts.Commands.UpdateContract;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Application.Features.Contracts.Queries;
using LawOfficeManagement.Application.Features.Contracts.Queries.GetAllContracts;
using LawOfficeManagement.Application.Features.Contracts.Queries.GetContractById;
using LawOfficeManagement.Core.Entities.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContractsController> _logger;

        public ContractsController(IMediator mediator, ILogger<ContractsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على جميع العقود مع إمكانية التصفية والترتيب
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContractDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ContractDto>>> GetAll(
            [FromQuery] string? searchTerm = null,
            [FromQuery] ContractStatus? status = null,
            [FromQuery] FinancialAgreementType? agreementType = null,
            [FromQuery] int? clientId = null,
            [FromQuery] int? caseId = null,
            [FromQuery] DateTime? startDateFrom = null,
            [FromQuery] DateTime? startDateTo = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("طلب جلب جميع العقود - الصفحة: {PageNumber}, الحجم: {PageSize}", pageNumber, pageSize);

                var query = new GetAllContractsQuery
                {
                    SearchTerm = searchTerm,
                    Status = status,
                    AgreementType = agreementType,
                    ClientId = clientId,
                    CaseId = caseId,
                    StartDateFrom = startDateFrom,
                    StartDateTo = startDateTo,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب العقود");
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// الحصول على عقد محدد بالمعرف
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContractDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ContractDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("طلب جلب العقد بالمعرف: {ContractId}", id);

                var query = new GetContractByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("العقد غير موجود: {ContractId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب العقد: {ContractId}", id);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// الحصول على عقود عميل محدد
        /// </summary>
        [HttpGet("client/{clientId}")]
        [ProducesResponseType(typeof(List<ContractDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<ContractDto>>> GetByClient(int clientId, [FromQuery] ContractStatus? status = null)
        {
            try
            {
                _logger.LogInformation("طلب جلب عقود العميل: {ClientId}", clientId);

                var query = new GetContractsByClientQuery
                {
                    ClientId = clientId,
                    Status = status
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب عقود العميل: {ClientId}", clientId);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// الحصول على إحصائيات العقود
        /// </summary>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(ContractsCountDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ContractsCountDto>> GetStatistics([FromQuery] int? clientId = null)
        {
            try
            {
                _logger.LogInformation("طلب جلب إحصائيات العقود - العميل: {ClientId}", clientId);

                var query = new GetContractsCountQuery { ClientId = clientId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في جلب إحصائيات العقود");
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// إنشاء عقد جديد
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<int>> Create([FromBody] CreateContractDto createContractDto)
        {
            try
            {
                _logger.LogInformation("طلب إنشاء عقد جديد: {ContractNumber}", createContractDto.ContractNumber);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات العقد غير صالحة");
                    return BadRequest(ModelState);
                }

                var command = new CreateContractCommand { ContractDto = createContractDto };
                var result = await _mediator.Send(command);

                _logger.LogInformation("تم إنشاء العقد بنجاح بالمعرف: {ContractId}", result);
                return Ok(new { id = result, message = "تم إنشاء العقد بنجاح" });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("فشل إنشاء العقد: {Error}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في إنشاء العقد");
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// تحديث عقد موجود
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateContractDto updateContractDto)
        {
            try
            {
                _logger.LogInformation("طلب تحديث العقد: {ContractId}", id);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("بيانات التحديث غير صالحة للعقد: {ContractId}", id);
                    return BadRequest(ModelState);
                }

                var command = new UpdateContractCommand
                {
                    Id = id,
                    ContractDto = updateContractDto
                };

                await _mediator.Send(command);

                _logger.LogInformation("تم تحديث العقد بنجاح: {ContractId}", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("العقد غير موجود للتحديث: {ContractId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("فشل تحديث العقد: {ContractId} - {Error}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تحديث العقد: {ContractId}", id);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// حذف عقد
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("طلب حذف العقد: {ContractId}", id);

                var command = new DeleteContractCommand { Id = id };
                await _mediator.Send(command);

                _logger.LogInformation("تم حذف العقد بنجاح: {ContractId}", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("العقد غير موجود للحذف: {ContractId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("فشل حذف العقد: {ContractId} - {Error}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في حذف العقد: {ContractId}", id);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// تغيير حالة العقد
        /// </summary>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> ChangeStatus(int id, [FromBody] ChangeContractStatusDto statusDto)
        {
            try
            {
                _logger.LogInformation("طلب تغيير حالة العقد: {ContractId} إلى {Status}", id, statusDto.Status);

                var contract = await _mediator.Send(new GetContractByIdQuery { Id = id });

                var updateDto = new UpdateContractDto
                {
                    Title = contract.Title,
                    ContractDescription = contract.ContractDescription,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    Status = statusDto.Status,
                    FinancialAgreementType = contract.FinancialAgreementType,
                    TotalCaseAmount = contract.TotalCaseAmount,
                    Percentage = contract.Percentage,
                    FinalAgreedAmount = contract.FinalAgreedAmount,
                    ContractDocumentUrl = contract.ContractDocumentUrl
                };

                var command = new UpdateContractCommand
                {
                    Id = id,
                    ContractDto = updateDto
                };

                await _mediator.Send(command);

                _logger.LogInformation("تم تغيير حالة العقد بنجاح: {ContractId} إلى {Status}", id, statusDto.Status);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("العقد غير موجود لتغيير الحالة: {ContractId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في تغيير حالة العقد: {ContractId}", id);
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    /// <summary>
    /// DTO لتغيير حالة العقد
    /// </summary>
    public class ChangeContractStatusDto
    {
        public ContractStatus Status { get; set; }
    }
}