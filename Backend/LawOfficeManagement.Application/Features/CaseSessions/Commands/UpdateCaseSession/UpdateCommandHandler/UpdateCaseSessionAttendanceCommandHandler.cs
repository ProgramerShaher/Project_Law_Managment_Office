using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionAttendanceCommandHandler : IRequestHandler<UpdateCaseSessionAttendanceCommand>
    {
        private readonly ILogger<UpdateCaseSessionAttendanceCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionAttendanceCommandHandler(
            ILogger<UpdateCaseSessionAttendanceCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionAttendanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating attendance for case session with ID: {SessionId}", request.Id);

                var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                caseSession.LawyerAttended = request.LawyerAttended;
                caseSession.ClientAttended = request.ClientAttended;
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System";

                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated attendance for case session with ID: {SessionId} - Lawyer: {Lawyer}, Client: {Client}",
                    request.Id, request.LawyerAttended, request.ClientAttended);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating attendance for case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}