// LawOfficeManagement.Application.Features.Opponents.Queries
using AutoMapper;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Opponents.Queries
{
    // Get All Opponents
    public class GetAllOpponentsQuery : IRequest<List<OpponentDto>>
    {
        public bool IncludeInactive { get; set; } = false;
    }

    public class GetAllOpponentsQueryHandler : IRequestHandler<GetAllOpponentsQuery, List<OpponentDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOpponentsQueryHandler> _logger;

        public GetAllOpponentsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllOpponentsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OpponentDto>> Handle(GetAllOpponentsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع الخصوم");

            var opponents = await _uow.Repository<Opponent>()
                .GetFilteredAsync(
                    filter: request.IncludeInactive ? null : o => !o.IsDeleted,
                    includeProperties: "cases",
                    orderBy: q => q.OrderBy(o => o.OpponentName)
                );

            return _mapper.Map<List<OpponentDto>>(opponents);
        }
    }

 

  
  

  
}