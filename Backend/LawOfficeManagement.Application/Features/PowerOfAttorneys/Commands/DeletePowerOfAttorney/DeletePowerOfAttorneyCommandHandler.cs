using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.DeletePowerOfAttorney
{
    public class DeletePowerOfAttorneyCommandHandler : IRequestHandler<DeletePowerOfAttorneyCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeletePowerOfAttorneyCommandHandler> _logger;

        public DeletePowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            ILogger<DeletePowerOfAttorneyCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(DeletePowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف الوكالة رقم {Id}", request.Id);

            var repo = _uow.Repository<PowerOfAttorney>();
            var entity = await repo.GetByIdAsync(request.Id);
            if (entity == null)
                throw new KeyNotFoundException($"الوكالة بالمعرف {request.Id} غير موجودة.");

            // حذف الملف من السيرفر
          

          await   repo.DeleteAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف الوكالة رقم {AgencyNumber}", entity.AgencyNumber);
            return true;
        }
    }
}
