using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetCaseById
{
    public class GetCaseByIdQuery : IRequest<CaseDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetCaseByIdQueryHandler : IRequestHandler<GetCaseByIdQuery, CaseDetailsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseByIdQueryHandler> _logger;

        public GetCaseByIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CaseDetailsDto> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب تفاصيل القضية {CaseId}", request.Id);

            // استخدام الـ Repository مع Include properties
            var caseEntity = await _uow.Repository<Case>()
                .FirstOrDefaultAsync(
                    predicate: c => c.Id == request.Id,
                    includeProperties: "Client,Court,CourtType,CourtDivision,Opponents,CaseType,PowerOfAttorney.Office,PowerOfAttorney.Lawyer"
                );

            if (caseEntity == null)
                throw new InvalidOperationException("القضية غير موجودة");

            var caseDetails = _mapper.Map<CaseDetailsDto>(caseEntity);

            _logger.LogInformation("تم جلب تفاصيل القضية {CaseId} بنجاح", request.Id);

            return caseDetails;
        }
    }
}