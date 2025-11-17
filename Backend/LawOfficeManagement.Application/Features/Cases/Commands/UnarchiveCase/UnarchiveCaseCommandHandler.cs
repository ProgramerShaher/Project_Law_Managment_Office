using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.UnarchiveCase
{
    public class UnarchiveCaseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class UnarchiveCaseCommandHandler : IRequestHandler<UnarchiveCaseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UnarchiveCaseCommandHandler> _logger;

        public UnarchiveCaseCommandHandler(
            IUnitOfWork uow,
            ILogger<UnarchiveCaseCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(UnarchiveCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إلغاء أرشفة القضية {CaseId}", request.Id);

            var caseEntity = await _uow.Repository<Case>()
                .GetByIdAsync(request.Id);

            if (caseEntity == null)
                throw new InvalidOperationException("القضية غير موجودة");

            caseEntity.IsArchived = false;

            await _uow.Repository<Case>().UpdateAsync(caseEntity);

            _logger.LogInformation("تم إلغاء أرشفة القضية {CaseId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}