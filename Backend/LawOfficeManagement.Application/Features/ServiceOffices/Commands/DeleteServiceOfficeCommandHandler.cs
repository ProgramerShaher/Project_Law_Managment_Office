using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.ServiceOffices.Commands
{
    public class DeleteServiceOfficeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteServiceOfficeCommandHandler : IRequestHandler<DeleteServiceOfficeCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteServiceOfficeCommandHandler> _logger;

        public DeleteServiceOfficeCommandHandler(IUnitOfWork uow, ILogger<DeleteServiceOfficeCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteServiceOfficeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف خدمة المكتب: {ServiceId}", request.Id);

            var service = await _uow.Repository<ServiceOffice>()
                .GetByIdAsync(request.Id);

            if (service == null || service.IsDeleted)
            {
                _logger.LogWarning("خدمة المكتب غير موجودة: {ServiceId}", request.Id);
                throw new KeyNotFoundException($"خدمة المكتب بالمعرف {request.Id} غير موجودة");
            }

            // التحقق من عدم وجود استشارات مرتبطة بالخدمة
            var hasConsultations = await _uow.Repository<LegalConsultation>()
                .ExistsAsync(lc => lc.ServiceOfficeId == request.Id && !lc.IsDeleted);

            if (hasConsultations)
            {
                _logger.LogWarning("لا يمكن حذف الخدمة لأنها مرتبطة باستشارات: {ServiceId}", request.Id);
                throw new InvalidOperationException("لا يمكن حذف الخدمة لأنها مرتبطة باستشارات قانونية");
            }

            service.IsDeleted = true;

            await _uow.Repository<ServiceOffice>().UpdateAsync(service);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف خدمة المكتب بنجاح: {ServiceId}", request.Id);

            return Unit.Value;
        }
    }
}
