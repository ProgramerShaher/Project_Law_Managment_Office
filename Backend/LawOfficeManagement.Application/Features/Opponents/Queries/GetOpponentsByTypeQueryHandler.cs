using AutoMapper;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Opponents.Queries
{
    // Get Opponents By Type
    public class GetOpponentsByTypeQuery : IRequest<List<OpponentDto>>
    {
        public OpponentType Type { get; set; }
    }

    public class GetOpponentsByTypeQueryHandler : IRequestHandler<GetOpponentsByTypeQuery, List<OpponentDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOpponentsByTypeQueryHandler> _logger;

        public GetOpponentsByTypeQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetOpponentsByTypeQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OpponentDto>> Handle(GetOpponentsByTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب الخصوم بنوع: {OpponentType}", request.Type);

            var opponents = await _uow.Repository<Opponent>()
                .GetFilteredAsync(
                    filter: o => o.Type == request.Type && !o.IsDeleted,
                    includeProperties: "cases",
                    orderBy: q => q.OrderBy(o => o.OpponentName)
                );

            return _mapper.Map<List<OpponentDto>>(opponents);
        }
    }

}
