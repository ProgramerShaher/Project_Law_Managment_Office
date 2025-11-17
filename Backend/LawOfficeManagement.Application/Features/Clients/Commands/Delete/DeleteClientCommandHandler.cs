using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Clients.Commands.Delete
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        private readonly ILogger<DeleteClientCommandHandler> _logger;

      

        public DeleteClientCommandHandler( IUnitOfWork uow, ILogger<DeleteClientCommandHandler> logger)
        {

            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _uow.Repository<Client>().GetByIdAsync(request.Id);
            if (client == null || client.IsDeleted)
                return false;
            client.IsDeleted = true;
            await _uow.Repository<Client>().DeleteAsync(client);
            await _uow.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("تم حذف العميل (حذف لطيف) {ClientId}", client.Id);
            return true;
        }
    }
}
