using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command
{
    public class UpdateCaseSessionAttendanceCommand : IRequest
    {
        public int Id { get; set; }
        public bool LawyerAttended { get; set; }
        public bool ClientAttended { get; set; }
    }

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
            var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
            if (caseSession == null)
            {
                throw new NotFoundException($"Case session with ID {request.Id} not found");
            }

            caseSession.LawyerAttended = request.LawyerAttended;
            caseSession.ClientAttended = request.ClientAttended;

            await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated attendance for case session with ID: {SessionId} - Lawyer: {Lawyer}, Client: {Client}",
                request.Id, request.LawyerAttended, request.ClientAttended);
        }
    }
}