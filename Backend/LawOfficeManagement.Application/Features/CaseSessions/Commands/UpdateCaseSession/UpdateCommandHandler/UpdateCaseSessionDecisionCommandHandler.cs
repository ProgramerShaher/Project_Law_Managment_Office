using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionDecisionCommandHandler : IRequestHandler<UpdateCaseSessionDecisionCommand>
    {
        private readonly ILogger<UpdateCaseSessionDecisionCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionDecisionCommandHandler(
            ILogger<UpdateCaseSessionDecisionCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionDecisionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating decision for case session with ID: {SessionId}", request.Id);

                var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                caseSession.Decision = request.Decision;
                caseSession.NextSessionDate = request.NextSessionDate;
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System";

                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated decision for case session with ID: {SessionId}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating decision for case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}