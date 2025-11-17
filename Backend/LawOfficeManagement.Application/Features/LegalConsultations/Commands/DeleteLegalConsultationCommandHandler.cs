using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.LegalConsultations.Commands
{
    public class DeleteLegalConsultationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteLegalConsultationCommandHandler : IRequestHandler<DeleteLegalConsultationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteLegalConsultationCommandHandler> _logger;

        public DeleteLegalConsultationCommandHandler(IUnitOfWork uow, ILogger<DeleteLegalConsultationCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteLegalConsultationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف الاستشارة القانونية: {ConsultationId}", request.Id);

            var consultation = await _uow.Repository<LegalConsultation>()
                .GetByIdAsync(request.Id);

            if (consultation == null || consultation.IsDeleted)
            {
                _logger.LogWarning("الاستشارة القانونية غير موجودة: {ConsultationId}", request.Id);
                throw new KeyNotFoundException($"الاستشارة القانونية بالمعرف {request.Id} غير موجودة");
            }

            consultation.IsDeleted = true;

            await _uow.Repository<LegalConsultation>().UpdateAsync(consultation);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف الاستشارة القانونية بنجاح: {ConsultationId}", request.Id);

            return Unit.Value;
        }
    }
}
