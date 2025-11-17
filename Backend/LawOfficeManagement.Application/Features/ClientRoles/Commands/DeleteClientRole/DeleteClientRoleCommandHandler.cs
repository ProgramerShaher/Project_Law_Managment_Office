using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.DeleteClientRole
{
    public class DeleteClientRoleCommandHandler : IRequestHandler<DeleteClientRoleCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteClientRoleCommandHandler> _logger;

        public DeleteClientRoleCommandHandler( IUnitOfWork uow, ILogger<DeleteClientRoleCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteClientRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _uow.Repository<ClientRole>().GetByIdAsync(request.Id);
            if (role == null || role.IsDeleted)
                return false;

            role.IsDeleted = true;
            await _uow.Repository<ClientRole>().UpdateAsync(role);
            await _uow.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("ClientRole soft-deleted: {RoleId}", role.Id);
            return true;
        }
    }
}
