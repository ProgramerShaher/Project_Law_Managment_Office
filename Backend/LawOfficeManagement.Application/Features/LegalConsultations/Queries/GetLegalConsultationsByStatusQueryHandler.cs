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
    public class GetLegalConsultationsByStatusQuery : IRequest<List<LegalConsultationDto>>
    {
        public string Status { get; set; }
    }

    public class GetLegalConsultationsByStatusQueryHandler : IRequestHandler<GetLegalConsultationsByStatusQuery, List<LegalConsultationDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLegalConsultationsByStatusQueryHandler> _logger;

        public GetLegalConsultationsByStatusQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetLegalConsultationsByStatusQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LegalConsultationDto>> Handle(GetLegalConsultationsByStatusQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب الاستشارات بحالة: {Status}", request.Status);

            var consultations = await _uow.Repository<LegalConsultation>()
                .GetFilteredAsync(
                    filter: lc => lc.Status == request.Status && !lc.IsDeleted,
                    includeProperties: "Lawyer,ServiceOffice",
                    orderBy: q => q.OrderByDescending(lc => lc.CreatedAt)
                );

            return _mapper.Map<List<LegalConsultationDto>>(consultations);
        }
    }
}
