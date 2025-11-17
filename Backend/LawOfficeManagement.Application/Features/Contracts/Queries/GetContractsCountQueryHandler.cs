using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Contracts.Queries
{
    public class GetContractsCountQuery : IRequest<ContractsCountDto>
    {
        public ContractStatus? Status { get; set; }
        public int? ClientId { get; set; }
    }
    public class GetContractsCountQueryHandler : IRequestHandler<GetContractsCountQuery, ContractsCountDto>
    {
        private readonly ILogger<GetContractsCountQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetContractsCountQueryHandler(
            ILogger<GetContractsCountQueryHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<ContractsCountDto> Handle(GetContractsCountQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب إحصائيات العقود");

            var total = await _uow.Repository<Contract>().CountAsync(request.ClientId.HasValue
                ? c => c.ClientId == request.ClientId.Value
                : null);

            var active = await _uow.Repository<Contract>().CountAsync(c =>
                c.Status == ContractStatus.Active &&
                (!request.ClientId.HasValue || c.ClientId == request.ClientId.Value));

            var completed = await _uow.Repository<Contract>().CountAsync(c =>
                c.Status == ContractStatus.Completed &&
                (!request.ClientId.HasValue || c.ClientId == request.ClientId.Value));

            var cancelled = await _uow.Repository<Contract>().CountAsync(c =>
                c.Status == ContractStatus.Cancelled &&
                (!request.ClientId.HasValue || c.ClientId == request.ClientId.Value));

            var result = new ContractsCountDto
            {
                Total = total,
                Active = active,
                Completed = completed,
                Cancelled = cancelled
            };

            _logger.LogInformation("تم جلب إحصائيات العقود: {Total} إجمالي, {Active} نشط", total, active);
            return result;
        }
    }
}

