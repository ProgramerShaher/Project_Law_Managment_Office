using LawOfficeManagement.Application.Features.CaseTypes.Commands.UpdateCaseType;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Commands.DeleteCaseType
{
    public class DeleteCaseTypeCommandHandler : IRequestHandler<DeleteCaseTypeCommand, Unit>
    {
        private readonly ILogger<DeleteCaseTypeCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public DeleteCaseTypeCommandHandler(
            ILogger<DeleteCaseTypeCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteCaseTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("»œ¡ ⁄„·Ì… Õ–› ‰Ê⁄ «·ﬁ÷Ì…: {CaseTypeId}", request.Id);

            var caseType = await _uow.Repository<CaseType>().GetByIdAsync(request.Id);

            if (caseType == null || caseType.IsDeleted)
            {
                _logger.LogWarning("‰Ê⁄ «·ﬁ÷Ì… €Ì— „ÊÃÊœ: {CaseTypeId}", request.Id);
                throw new InvalidOperationException($"‰Ê⁄ «·ﬁ÷Ì… »«·„⁄—› {request.Id} €Ì— „ÊÃÊœ.");
            }

            // «· Õﬁﬁ „‰ ⁄œ„ ÊÃÊœ ﬁ÷«Ì« „— »ÿ…
            var hasCases = await _uow.Repository<Core.Entities.Cases.Case>().ExistsAsync(c =>
                !c.IsDeleted && c.CaseTypeId == request.Id);

            if (hasCases)
            {
                _logger.LogWarning("·« Ì„ﬂ‰ Õ–› ‰Ê⁄ «·ﬁ÷Ì… ·√‰Â „— »ÿ »ﬁ÷«Ì«: {CaseTypeId}", request.Id);
                throw new InvalidOperationException("·« Ì„ﬂ‰ Õ–› ‰Ê⁄ «·ﬁ÷Ì… ·√‰Â „— »ÿ »ﬁ÷«Ì« „ÊÃÊœ….");
            }

            // Õ–› „‰ÿﬁÌ
            caseType.IsDeleted = true;

            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(" „ Õ–› ‰Ê⁄ «·ﬁ÷Ì… »‰Ã«Õ: {CaseTypeId}", request.Id);
            return Unit.Value;
        }
    }
}