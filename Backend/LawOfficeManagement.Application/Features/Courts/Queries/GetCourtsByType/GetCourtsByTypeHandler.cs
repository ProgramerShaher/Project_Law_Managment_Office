using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtsByType
{
    public class GetCourtsByTypeHandler : IRequestHandler<GetCourtsByTypeQuery, IEnumerable<CourtDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCourtsByTypeHandler> _logger;

        public GetCourtsByTypeHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetCourtsByTypeHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CourtDto>> Handle(GetCourtsByTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب المحاكم حسب نوع المحكمة {CourtTypeId}", request.CourtTypeId);

            var courts = await _uow.Repository<Court>()
                .GetFilteredAsync(
                    filter: c => c.CourtTypeId == request.CourtTypeId && !c.IsDeleted,
                    includeProperties: "CourtType,Divisions"
                );
            var result = _mapper.Map<IEnumerable<CourtDto>>(courts);

            _logger.LogInformation("تم جلب {Count} محكمة بنوع المحكمة {CourtTypeId}", result.Count(), request.CourtTypeId);
            return result;
        }
    }
}