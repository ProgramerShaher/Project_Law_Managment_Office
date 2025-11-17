using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.LegalConsultations.Queries
{
    public class GetLegalConsultationByIdQuery : IRequest<LegalConsultationDto>
    {
        public int Id { get; set; }
    }

    public class GetLegalConsultationByIdQueryHandler : IRequestHandler<GetLegalConsultationByIdQuery, LegalConsultationDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLegalConsultationByIdQueryHandler> _logger;

        public GetLegalConsultationByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetLegalConsultationByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LegalConsultationDto> Handle(GetLegalConsultationByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب الاستشارة القانونية بالمعرف: {ConsultationId}", request.Id);

            var consultation = await _uow.Repository<LegalConsultation>()
                .FirstOrDefaultAsync(
                    lc => lc.Id == request.Id && !lc.IsDeleted,
                    "Lawyer,ServiceOffice"
                );

            if (consultation == null)
            {
                _logger.LogWarning("الاستشارة القانونية غير موجودة: {ConsultationId}", request.Id);
                throw new KeyNotFoundException($"الاستشارة القانونية بالمعرف {request.Id} غير موجودة");
            }

            return _mapper.Map<LegalConsultationDto>(consultation);
        }
    }
}
