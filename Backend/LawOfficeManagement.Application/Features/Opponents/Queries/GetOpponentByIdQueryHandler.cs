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
    // Get Opponent By Id
    public class GetOpponentByIdQuery : IRequest<OpponentDto>
    {
        public int Id { get; set; }
    }

    public class GetOpponentByIdQueryHandler : IRequestHandler<GetOpponentByIdQuery, OpponentDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOpponentByIdQueryHandler> _logger;

        public GetOpponentByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetOpponentByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OpponentDto> Handle(GetOpponentByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب الخصم بالمعرف: {OpponentId}", request.Id);

            var opponent = await _uow.Repository<Opponent>()
                .FirstOrDefaultAsync(
                    o => o.Id == request.Id && !o.IsDeleted,
                    "cases"
                );

            if (opponent == null)
            {
                _logger.LogWarning("الخصم غير موجود: {OpponentId}", request.Id);
                throw new KeyNotFoundException($"الخصم بالمعرف {request.Id} غير موجود");
            }

            return _mapper.Map<OpponentDto>(opponent);
        }
    }
}
