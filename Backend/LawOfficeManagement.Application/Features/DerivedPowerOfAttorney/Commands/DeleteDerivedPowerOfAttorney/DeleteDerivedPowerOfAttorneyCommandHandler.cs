using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.DeleteDerivedPowerOfAttorney
{
    public class DeleteDerivedPowerOfAttorneyCommandHandler : IRequestHandler<DeleteDerivedPowerOfAttorneyCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteDerivedPowerOfAttorneyCommandHandler> _logger;

        public DeleteDerivedPowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            ILogger<DeleteDerivedPowerOfAttorneyCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDerivedPowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف الوكالة المشتقة {Id}", request.Id);

            var entity = await _uow.Repository<DerivedPowerOfAttorney>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                throw new InvalidOperationException("الوكالة المشتقة غير موجودة أو محذوفة بالفعل");

            // حذف الملف المرفوع إذا كان موجوداً
         

            entity.IsDeleted = true;
           await _uow.Repository<DerivedPowerOfAttorney>().UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف الوكالة المشتقة {Id} بنجاح", request.Id);
            return Unit.Value;
        }
    }
}