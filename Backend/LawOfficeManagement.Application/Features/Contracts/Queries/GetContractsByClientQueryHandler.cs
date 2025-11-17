using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.Contracts.Queries
{
    public class GetContractsByClientQuery : IRequest<List<ContractDto>>
    {
        public int ClientId { get; set; }
        public ContractStatus? Status { get; set; } = ContractStatus.Active;
    }
    public class GetContractsByClientQueryHandler : IRequestHandler<GetContractsByClientQuery, List<ContractDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetContractsByClientQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetContractsByClientQueryHandler(
            IMapper mapper,
            ILogger<GetContractsByClientQueryHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<List<ContractDto>> Handle(GetContractsByClientQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب عقود العميل: {ClientId}", request.ClientId);

            Expression<Func<Contract, bool>>? filter = request.Status.HasValue
                ? c => c.ClientId == request.ClientId && c.Status == request.Status.Value
                : c => c.ClientId == request.ClientId;

            var contracts = await _uow.Repository<Contract>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.StartDate),
                    includeProperties: "Client,Case"
                );

            var contractsDto = _mapper.Map<List<ContractDto>>(contracts);

            _logger.LogInformation("تم جلب {Count} عقد للعميل {ClientId}", contractsDto.Count, request.ClientId);
            return contractsDto;
        }
    }
}

