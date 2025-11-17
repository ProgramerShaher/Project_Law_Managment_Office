using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionStatusCommandHandler : IRequestHandler<UpdateCaseSessionStatusCommand>
    {
        private readonly ILogger<UpdateCaseSessionStatusCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionStatusCommandHandler(
            ILogger<UpdateCaseSessionStatusCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating status for case session with ID: {SessionId} to {Status}",
                    request.Id, request.SessionStatus);

                var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                caseSession.SessionStatus = request.SessionStatus;
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System";

                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated status for case session with ID: {SessionId} to {Status}",
                    request.Id, request.SessionStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating status for case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}