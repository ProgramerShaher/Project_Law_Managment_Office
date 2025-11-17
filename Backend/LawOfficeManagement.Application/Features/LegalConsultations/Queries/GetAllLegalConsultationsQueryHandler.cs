// LawOfficeManagement.Application.Features.LegalConsultations.Queries
using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.LegalConsultations.Queries
{
    // Get All Legal Consultations
    public class GetAllLegalConsultationsQuery : IRequest<List<LegalConsultationDto>>
    {
        public bool IncludeInactive { get; set; } = false;
    }

    public class GetAllLegalConsultationsQueryHandler : IRequestHandler<GetAllLegalConsultationsQuery, List<LegalConsultationDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllLegalConsultationsQueryHandler> _logger;

        public GetAllLegalConsultationsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllLegalConsultationsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LegalConsultationDto>> Handle(GetAllLegalConsultationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع الاستشارات القانونية");

            var consultations = await _uow.Repository<LegalConsultation>()
                .GetFilteredAsync(
                    filter: request.IncludeInactive ? null : lc => !lc.IsDeleted,
                    includeProperties: "Lawyer,ServiceOffice",
                    orderBy: q => q.OrderByDescending(lc => lc.CreatedAt)
                );

            return _mapper.Map<List<LegalConsultationDto>>(consultations);
        }
    }

    // Get Legal Consultation By Id
  


  
}