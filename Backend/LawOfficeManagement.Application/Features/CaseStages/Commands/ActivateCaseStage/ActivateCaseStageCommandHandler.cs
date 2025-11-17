using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.ActivateCaseStage
{
    public class ActivateCaseStageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class ActivateCaseStageCommandHandler : IRequestHandler<ActivateCaseStageCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<ActivateCaseStageCommandHandler> _logger;

        public ActivateCaseStageCommandHandler(
            IUnitOfWork uow,
            ILogger<ActivateCaseStageCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(ActivateCaseStageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تفعيل المرحلة {StageId}", request.Id);

            var caseStage = await _uow.Repository<CaseStage>()
                .GetByIdAsync(request.Id);

            if (caseStage == null)
                throw new InvalidOperationException("المرحلة غير موجودة");

            // إلغاء تفعيل جميع مراحل القضية
            var allStages = await _uow.Repository<CaseStage>()
                .GetAsync(cs => cs.CaseId == caseStage.CaseId);

            foreach (var stage in allStages)
            {
                stage.IsActive = false;
                await _uow.Repository<CaseStage>().UpdateAsync(stage);
            }

            // تفعيل المرحلة المطلوبة
            caseStage.IsActive = true;

            await _uow.Repository<CaseStage>().UpdateAsync(caseStage);

            _logger.LogInformation("تم تفعيل المرحلة {StageId} بنجاح للقضية {CaseId}",
                request.Id, caseStage.CaseId);

            return Unit.Value;
        }
    }
}