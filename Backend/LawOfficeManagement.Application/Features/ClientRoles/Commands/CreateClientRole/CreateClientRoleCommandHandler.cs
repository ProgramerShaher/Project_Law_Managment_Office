using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.CreateClientRole
{
    public class CreateClientRoleCommandHandler : IRequestHandler<CreateClientRoleCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateClientRoleCommandHandler> _logger;

        public CreateClientRoleCommandHandler(
            IUnitOfWork uow,
            ILogger<CreateClientRoleCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<int> Handle(CreateClientRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");

            var exists = await _uow.Repository<ClientRole>().ExistsAsync(r => r.Name == request.Name && !r.IsDeleted);
            if (exists)
                throw new InvalidOperationException($"Role '{request.Name}' already exists");

            var role = new ClientRole { Name = request.Name.Trim() };
            await _uow.Repository<ClientRole>().AddAsync(role);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("ClientRole created: {RoleId} - {Name}", role.Id, role.Name);
            return role.Id;
        }
    }
}
