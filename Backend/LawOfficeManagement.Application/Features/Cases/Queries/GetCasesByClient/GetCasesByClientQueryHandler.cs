using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetCasesByClient
{
    public class GetCasesByClientQuery : IRequest<List<CaseListDto>>
    {
        public int ClientId { get; set; }
        public bool IncludeArchived { get; set; } = false;
    }
    public class GetCasesByClientQueryHandler : IRequestHandler<GetCasesByClientQuery, List<CaseListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCasesByClientQueryHandler> _logger;

        public GetCasesByClientQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCasesByClientQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseListDto>> Handle(GetCasesByClientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب قضايا العميل {ClientId} - تضمين المؤرشفة: {IncludeArchived}",
                request.ClientId, request.IncludeArchived);

            // التحقق من وجود العميل أولاً
            var clientExists = await _uow.Repository<Client>()
                .ExistsAsync(c => c.Id == request.ClientId && !c.IsDeleted);

            if (!clientExists)
            {
                _logger.LogWarning("العميل غير موجود أو محذوف: {ClientId}", request.ClientId);
                throw new InvalidOperationException("العميل غير موجود أو محذوف");
            }

            // بناء التعبير للتصفية
            System.Linq.Expressions.Expression<Func<Case, bool>> filter;

            if (request.IncludeArchived)
            {
                filter = c => c.ClientId == request.ClientId;
            }
            else
            {
                filter = c => c.ClientId == request.ClientId && !c.IsArchived;
            }

            var cases = await _uow.Repository<Case>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.CreatedAt),
                    includeProperties: "Client,Court,CaseType"
                );

            var caseList = _mapper.Map<List<CaseListDto>>(cases);

            _logger.LogInformation("تم جلب {Count} قضية للعميل {ClientId}", caseList.Count, request.ClientId);

            return caseList;
        }
    }
}