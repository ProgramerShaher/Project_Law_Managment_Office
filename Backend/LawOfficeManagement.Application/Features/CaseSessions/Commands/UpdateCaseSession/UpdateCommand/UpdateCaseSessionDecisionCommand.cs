using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command
{
    public class UpdateCaseSessionDecisionCommand : IRequest
    {
        public int Id { get; set; }
        public string? Decision { get; set; }
        public DateTime? NextSessionDate { get; set; }
    }

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
            var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
            if (caseSession == null)
            {
                throw new NotFoundException($"Case session with ID {request.Id} not found");
            }

            caseSession.Decision = request.Decision;
            caseSession.NextSessionDate = request.NextSessionDate;

            await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated decision for case session with ID: {SessionId}", request.Id);
        }
    }
}