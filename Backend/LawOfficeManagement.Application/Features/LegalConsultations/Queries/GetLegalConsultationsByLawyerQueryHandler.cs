using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
namespace LawOfficeManagement.Application.Features.LegalConsultations.Queries
{
    public class GetLegalConsultationsByLawyerQuery : IRequest<List<LegalConsultationDto>>
    {
        public int LawyerId { get; set; }
    }

    public class GetLegalConsultationsByLawyerQueryHandler : IRequestHandler<GetLegalConsultationsByLawyerQuery, List<LegalConsultationDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLegalConsultationsByLawyerQueryHandler> _logger;

        public GetLegalConsultationsByLawyerQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetLegalConsultationsByLawyerQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LegalConsultationDto>> Handle(GetLegalConsultationsByLawyerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب استشارات المحامي: {LawyerId}", request.LawyerId);

            var consultations = await _uow.Repository<LegalConsultation>()
                .GetFilteredAsync(
                    filter: lc => lc.LawyerId == request.LawyerId && !lc.IsDeleted,
                    includeProperties: "Lawyer,ServiceOffice",
                    orderBy: q => q.OrderByDescending(lc => lc.CreatedAt)
                );

            return _mapper.Map<List<LegalConsultationDto>>(consultations);
        }
    }
}