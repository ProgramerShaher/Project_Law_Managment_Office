using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.Cases.Queries.GetAllCases
{
    public class GetAllCasesQuery : IRequest<List<CaseListDto>>
    {
        public bool IncludeArchived { get; set; } = false;
    }

    public class GetAllCasesQueryHandler : IRequestHandler<GetAllCasesQuery, List<CaseListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCasesQueryHandler> _logger;

        public GetAllCasesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetAllCasesQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseListDto>> Handle(GetAllCasesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع القضايا - تضمين المؤرشفة: {IncludeArchived}", request.IncludeArchived);

            Expression<Func<Case, bool>>? filter = null;

            if (!request.IncludeArchived)
            {
                filter = c => !c.IsArchived;
            }

            var cases = await _uow.Repository<Case>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.CreatedAt),
                    includeProperties: "Client,Court,CaseType"
                );

            var caseList = _mapper.Map<List<CaseListDto>>(cases);

            _logger.LogInformation("تم جلب {Count} قضية بنجاح", caseList.Count);

            return caseList;
        }
    }
}