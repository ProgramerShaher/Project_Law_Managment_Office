// LawOfficeManagement.Application.Features.LegalConsultations.Commands
using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.LegalConsultations.Commands
{
    // Create Legal Consultation
    public class CreateLegalConsultationCommand : IRequest<int>
    {
        public CreateLegalConsultationDto CreateDto { get; set; }
    }

    public class CreateLegalConsultationCommandHandler : IRequestHandler<CreateLegalConsultationCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateLegalConsultationCommandHandler> _logger;

        public CreateLegalConsultationCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateLegalConsultationCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateLegalConsultationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء استشارة قانونية جديدة للعميل: {CustomerName}", request.CreateDto.CustomerName);

            // التحقق من وجود المحامي
            var lawyerExists = await _uow.Repository<Lawyer>()
                .ExistsAsync(l => l.Id == request.CreateDto.LawyerId && !l.IsDeleted);

            if (!lawyerExists)
            {
                _logger.LogWarning("المحامي غير موجود: {LawyerId}", request.CreateDto.LawyerId);
                throw new KeyNotFoundException($"المحامي بالمعرف {request.CreateDto.LawyerId} غير موجود");
            }

            // التحقق من وجود الخدمة
            var serviceExists = await _uow.Repository<Core.Entities.Cases.ServiceOffice>()
                .ExistsAsync(s => s.Id == request.CreateDto.ServiceOfficeId && !s.IsDeleted);

            if (!serviceExists)
            {
                _logger.LogWarning("الخدمة غير موجودة: {ServiceId}", request.CreateDto.ServiceOfficeId);
                throw new KeyNotFoundException($"الخدمة بالمعرف {request.CreateDto.ServiceOfficeId} غير موجودة");
            }

            var consultation = _mapper.Map<LegalConsultation>(request.CreateDto);

            await _uow.Repository<LegalConsultation>().AddAsync(consultation);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء الاستشارة القانونية بنجاح بالمعرف: {ConsultationId}", consultation.Id);

            return consultation.Id;
        }
    }

   
  
}