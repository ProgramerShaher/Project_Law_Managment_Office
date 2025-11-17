using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.DeleteCase
{
    public class DeleteCaseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class DeleteCaseCommandHandler : IRequestHandler<DeleteCaseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteCaseCommandHandler> _logger;

        public DeleteCaseCommandHandler(
            IUnitOfWork uow,
            ILogger<DeleteCaseCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف القضية {CaseId}", request.Id);

            var caseEntity = await _uow.Repository<Case>()
                .GetByIdAsync(request.Id);

            if (caseEntity == null)
                throw new InvalidOperationException("القضية غير موجودة");

            // التحقق من عدم وجود ارتباطات قبل الحذف
            if (caseEntity.CaseSessions?.Any() == true)
                throw new InvalidOperationException("لا يمكن حذف القضية لأنها تحتوي على جلسات");

            if (caseEntity.CaseDocuments?.Any() == true)
                throw new InvalidOperationException("لا يمكن حذف القضية لأنها تحتوي على مستندات");

         await   _uow.Repository<Case>().DeleteAsync(caseEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف القضية {CaseId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}