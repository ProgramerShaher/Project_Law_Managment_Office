using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
namespace LawOfficeManagement.Application.Features.LegalConsultations.Commands
{
    public class UpdateLegalConsultationCommand : IRequest<Unit>
    {
        public UpdateLegalConsultationDto UpdateDto { get; set; }
    }

    public class UpdateLegalConsultationCommandHandler : IRequestHandler<UpdateLegalConsultationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateLegalConsultationCommandHandler> _logger;

        public UpdateLegalConsultationCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateLegalConsultationCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateLegalConsultationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث الاستشارة القانونية: {ConsultationId}", request.UpdateDto.Id);

            var consultation = await _uow.Repository<LegalConsultation>()
                .GetByIdAsync(request.UpdateDto.Id);

            if (consultation == null || consultation.IsDeleted)
            {
                _logger.LogWarning("الاستشارة القانونية غير موجودة: {ConsultationId}", request.UpdateDto.Id);
                throw new KeyNotFoundException($"الاستشارة القانونية بالمعرف {request.UpdateDto.Id} غير موجودة");
            }

            // التحقق من وجود المحامي إذا تم التغيير
            if (consultation.LawyerId != request.UpdateDto.LawyerId)
            {
                var lawyerExists = await _uow.Repository<Lawyer>()
                    .ExistsAsync(l => l.Id == request.UpdateDto.LawyerId && !l.IsDeleted);

                if (!lawyerExists)
                {
                    _logger.LogWarning("المحامي غير موجود: {LawyerId}", request.UpdateDto.LawyerId);
                    throw new KeyNotFoundException($"المحامي بالمعرف {request.UpdateDto.LawyerId} غير موجود");
                }
            }

            // التحقق من وجود الخدمة إذا تم التغيير
            if (consultation.ServiceOfficeId != request.UpdateDto.ServiceOfficeId)
            {
                var serviceExists = await _uow.Repository<ServiceOffice>()
                    .ExistsAsync(s => s.Id == request.UpdateDto.ServiceOfficeId && !s.IsDeleted);

                if (!serviceExists)
                {
                    _logger.LogWarning("الخدمة غير موجودة: {ServiceId}", request.UpdateDto.ServiceOfficeId);
                    throw new KeyNotFoundException($"الخدمة بالمعرف {request.UpdateDto.ServiceOfficeId} غير موجودة");
                }
            }

            _mapper.Map(request.UpdateDto, consultation);

            await _uow.Repository<LegalConsultation>().UpdateAsync(consultation);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث الاستشارة القانونية بنجاح: {ConsultationId}", consultation.Id);

            return Unit.Value;
        }
    }
}