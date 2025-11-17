using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.DeleteCaseStage
{
    public class DeleteCaseStageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class DeleteCaseStageCommandHandler : IRequestHandler<DeleteCaseStageCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteCaseStageCommandHandler> _logger;

        public DeleteCaseStageCommandHandler(
            IUnitOfWork uow,
            ILogger<DeleteCaseStageCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCaseStageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف المرحلة {StageId}", request.Id);

            var caseStage = await _uow.Repository<CaseStage>()
                .GetByIdAsync(request.Id);

            if (caseStage == null)
                throw new InvalidOperationException("المرحلة غير موجودة");

            // لا يمكن حذف المرحلة النشطة
            if (caseStage.IsActive)
                throw new InvalidOperationException("لا يمكن حذف المرحلة النشطة");

            await _uow.Repository<CaseStage>().DeleteAsync(caseStage);

            _logger.LogInformation("تم حذف المرحلة {StageId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}