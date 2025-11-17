using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.ArchiveCase
{
    public class ArchiveCaseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class ArchiveCaseCommandHandler : IRequestHandler<ArchiveCaseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<ArchiveCaseCommandHandler> _logger;

        public ArchiveCaseCommandHandler(
            IUnitOfWork uow,
            ILogger<ArchiveCaseCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(ArchiveCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء أرشفة القضية {CaseId}", request.Id);

            var caseEntity = await _uow.Repository<Case>()
                .GetByIdAsync(request.Id);

            if (caseEntity == null)
                throw new InvalidOperationException("القضية غير موجودة");

            caseEntity.IsArchived = true;

            await _uow.Repository<Case>().UpdateAsync(caseEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم أرشفة القضية {CaseId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}