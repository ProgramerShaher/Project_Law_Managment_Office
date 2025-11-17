using LawOfficeManagement.Application.Features.Clients.Commands.CreateClient;
using LawOfficeManagement.Application.Features.Clients.Commands.Update;
using LawOfficeManagement.Application.Features.Clients.Commands.Delete;
using LawOfficeManagement.Application.Features.Clients.Queries.GetClientById;
using LawOfficeManagement.Application.Features.Clients.Queries.GetAllClients;
using MediatR;
using Microsoft.AspNetCore.Authorization; // أضف هذا
using Microsoft.AspNetCore.Mvc;

namespace LawOfficeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // تأمين كل نقاط النهاية في هذا المتحكم
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());
            return Ok(result);
        }

        [HttpPost]
        // يمكن تحديد أدوار معينة لها صلاحية الإنشاء
        // [Authorize(Roles = "Administrator,Lawyer")] 
        public async Task<IActionResult> CreateClient( CreateClientCommand command)
        {
            // استخدام [FromForm] ضروري لاستقبال الملفات (IFormFile)

            _logger.LogInformation("بدء طلب إنشاء عميل جديد.");

            // لا حاجة لكتلة try-catch هنا إذا كان لديك Middleware لمعالجة الأخطاء
            var clientId = await _mediator.Send(command);

            _logger.LogInformation("تم إنشاء العميل بنجاح بالمعرف: {ClientId}", clientId);

            // إرجاع 201 Created مع رابط للكيان الجديد ومعرف الكيان
            return CreatedAtAction(nameof(GetClientById), new { id = clientId }, new { ClientId = clientId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromForm] UpdateClientCommand command)
        {
            if (id != command.Id) return BadRequest();
            var ok = await _mediator.Send(command);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var ok = await _mediator.Send(new DeleteClientCommand { Id = id });
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
