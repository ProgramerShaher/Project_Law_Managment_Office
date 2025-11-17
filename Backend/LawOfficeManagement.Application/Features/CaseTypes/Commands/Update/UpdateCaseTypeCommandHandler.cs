using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Commands.UpdateCaseType
{
    public class UpdateCaseTypeCommandHandler : IRequestHandler<UpdateCaseTypeCommand, Unit>
    {
        private readonly ILogger<UpdateCaseTypeCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseTypeCommandHandler(
            ILogger<UpdateCaseTypeCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateCaseTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية تحديث نوع القضية: {CaseTypeId}", request.Id);

            // البحث عن نوع القضية
            var caseType = await _uow.Repository<CaseType>().GetByIdAsync(request.Id);

            if (caseType == null)
            {
                _logger.LogWarning("نوع القضية غير موجود: {CaseTypeId}", request.Id);
                throw new InvalidOperationException($"نوع القضية بالمعرف {request.Id} غير موجود.");
            }

            // التحقق من عدم تكرار الاسم (إذا تم تغييره)
            if (caseType.Name != request.Name)
            {
                var nameExists = await _uow.Repository<CaseType>().ExistsAsync(ct =>
                    ct.Name == request.Name && ct.Id != request.Id);

                if (nameExists)
                {
                    _logger.LogWarning("فشلت محاولة تحديث نوع قضية باسم موجود بالفعل: {Name}", request.Name);
                    throw new InvalidOperationException($"نوع القضية '{request.Name}' موجود بالفعل.");
                }
            }

            // تحديث البيانات
            caseType.Name = request.Name;
            caseType.Description = request.Description;

            await _uow.Repository<CaseType>().UpdateAsync(caseType);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث نوع القضية بنجاح: {CaseTypeId}", request.Id);
            return Unit.Value;
        }
    }
}