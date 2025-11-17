using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.UpdateClientRole
{
    public class UpdateClientRoleCommandHandler : IRequestHandler<UpdateClientRoleCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateClientRoleCommandHandler> _logger;

        public UpdateClientRoleCommandHandler( IUnitOfWork uow, ILogger<UpdateClientRoleCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateClientRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _uow.Repository<ClientRole>().GetByIdAsync(request.Id);
            if (role == null || role.IsDeleted)
                return false;

            var duplicate = await _uow.Repository<ClientRole>().ExistsAsync(r => r.Id != request.Id && r.Name == request.Name && !r.IsDeleted);
            if (duplicate)
                throw new InvalidOperationException($"Role '{request.Name}' already exists");

            role.Name = request.Name.Trim();
            await _uow.Repository<ClientRole>().UpdateAsync(role);
            await _uow.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("ClientRole updated: {RoleId}", role.Id);
            return true;
        }
    }
}
