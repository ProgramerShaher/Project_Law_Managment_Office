using AutoMapper;
using AutoMapper.QueryableExtensions;
using LawOfficeManagement.Core.Entities; 
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientSummaryDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllClientsQueryHandler> _logger;

        public GetAllClientsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllClientsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ClientSummaryDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب قائمة العملاء.");

            var entities = await _uow.Repository<Client>().GetAsync(c => !c.IsDeleted);
            var dtos = _mapper.Map<List<ClientSummaryDto>>(entities);

            _logger.LogInformation("تم جلب {ClientCount} عميل بنجاح.", dtos.Count);
            return dtos;
        }
    }
}
