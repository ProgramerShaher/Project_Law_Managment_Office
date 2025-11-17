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
    public class SearchOpponentsQuery : IRequest<List<OpponentDto>>
    {
        public string SearchTerm { get; set; }
    }

    public class SearchOpponentsQueryHandler : IRequestHandler<SearchOpponentsQuery, List<OpponentDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchOpponentsQueryHandler> _logger;

        public SearchOpponentsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<SearchOpponentsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OpponentDto>> Handle(SearchOpponentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بحث في الخصوم بالمصطلح: {SearchTerm}", request.SearchTerm);

            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                return new List<OpponentDto>();
            }

            var opponents = await _uow.Repository<Opponent>()
                .GetFilteredAsync(
                    filter: o => !o.IsDeleted &&
                                (o.OpponentName.Contains(request.SearchTerm) ||
                                 o.OpponentMobile.Contains(request.SearchTerm) ||
                                 o.OpponentLawyer.Contains(request.SearchTerm)),
                    includeProperties: "cases",
                    orderBy: q => q.OrderBy(o => o.OpponentName)
                );

            return _mapper.Map<List<OpponentDto>>(opponents);
        }
    }
}
