using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.Delete
{
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
            try
            {
                _logger.LogInformation("Deleting case session with ID: {SessionId}", request.Id);

                var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                // Soft Delete
                caseSession.IsDeleted = true;
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System"; // يمكنك استبدالها بالمستخدم الحالي

                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted case session with ID: {SessionId}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}
