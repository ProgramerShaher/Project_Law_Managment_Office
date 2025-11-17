using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtDivisionsByCourt
{
    public class GetCourtDivisionsByCourtHandler : IRequestHandler<GetCourtDivisionsByCourtQuery, IEnumerable<CourtDivisionDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourtDivisionsByCourtHandler> _logger;

        public GetCourtDivisionsByCourtHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetCourtDivisionsByCourtHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CourtDivisionDto>> Handle(GetCourtDivisionsByCourtQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب أقسام المحكمة {CourtId}", request.CourtId);

            var divisions = await _uow.Repository<CourtDivision>()
                .GetFilteredAsync(
                    filter: d => d.CourtId == request.CourtId && !d.IsDeleted,
                    includeProperties: "Court"
                );

            var result = _mapper.Map<IEnumerable<CourtDivisionDto>>(divisions);

            _logger.LogInformation("تم جلب {Count} قسم للمحكمة {CourtId}", result.Count(), request.CourtId);
            return result;
        }
    }
}