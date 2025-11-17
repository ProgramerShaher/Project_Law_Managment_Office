using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Queries.GetLawyersAvailableForCase
{

    public class GetLawyersAvailableForCaseQuery : IRequest<List<AvailableLawyerDto>>
    {
        public int CaseId { get; set; }
    }
    public class GetLawyersAvailableForCaseQueryHandler
        : IRequestHandler<GetLawyersAvailableForCaseQuery, List<AvailableLawyerDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLawyersAvailableForCaseQueryHandler> _logger;

        public GetLawyersAvailableForCaseQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetLawyersAvailableForCaseQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AvailableLawyerDto>> Handle(
            GetLawyersAvailableForCaseQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب المحامين المتاحين للقضية {CaseId}", request.CaseId);

            // جلب جميع المحامين النشطين
            var allLawyers = await _uow.Repository<Lawyer>()
                .GetAsync(l => !l.IsDeleted);

            // جلب المحامين المضافين مسبقاً للقضية
            var assignedLawyers = await _uow.Repository<CaseTeam>()
                .GetAsync(ct => ct.CaseId == request.CaseId && ct.IsActive);

            var assignedLawyerIds = assignedLawyers.Select(ct => ct.LawyerId).ToList();

            // تصفية المحامين غير المضافين للقضية
            var availableLawyers = allLawyers
                .Where(l => !assignedLawyerIds.Contains(l.Id))
                .ToList();

            var result = new List<AvailableLawyerDto>();

            foreach (var lawyer in availableLawyers)
            {
                var lawyerDto = _mapper.Map<AvailableLawyerDto>(lawyer);

                // البحث عن وكالة مشتقة للمحامي مرتبطة بنفس القضية
                var derivedPowerOfAttorney = await _uow.Repository<DerivedPowerOfAttorney>()
                    .FirstOrDefaultAsync(
                        predicate: poa => poa.LawyerId == lawyer.Id &&
                                         poa.ParentPowerOfAttorney.ClientId== request.CaseId &&
                                         poa.IsActive,
                        includeProperties: ""
                    );


                result.Add(lawyerDto);
            }

            _logger.LogInformation("تم جلب {Count} محامٍ متاح للقضية {CaseId}", result.Count, request.CaseId);

            return result;
        }
    }
}