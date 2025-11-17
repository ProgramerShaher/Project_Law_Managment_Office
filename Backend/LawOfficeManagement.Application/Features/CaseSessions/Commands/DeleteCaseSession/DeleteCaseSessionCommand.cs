using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class DeleteCaseSessionCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCaseSessionCommandHandler : IRequestHandler<DeleteCaseSessionCommand>
    {
        private readonly ILogger<DeleteCaseSessionCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public DeleteCaseSessionCommandHandler(
            ILogger<DeleteCaseSessionCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(DeleteCaseSessionCommand request, CancellationToken cancellationToken)
        {
            var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
            if (caseSession == null)
            {
                throw new NotFoundException($"Case session with ID {request.Id} not found");
            }

            caseSession.IsDeleted = true;

            await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully deleted case session with ID: {SessionId}", request.Id);
        }
    }
}