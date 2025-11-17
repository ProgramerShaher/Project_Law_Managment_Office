using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Enums;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command
{
    public class UpdateCaseSessionStatusCommand : IRequest
    {
        public int Id { get; set; }
        public CaseSessionStatus SessionStatus { get; set; }
    }

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
            var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
            if (caseSession == null)
            {
                throw new NotFoundException($"Case session with ID {request.Id} not found");
            }

            caseSession.SessionStatus = request.SessionStatus;

            await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated status for case session with ID: {SessionId} to {Status}",
                request.Id, request.SessionStatus);
        }
    }
}