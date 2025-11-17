using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LawOfficeManagement.Application.Features.Contracts.Queries.GetAllContracts
{
    public class GetAllContractsQuery : IRequest<List<ContractDto>>
    {
        public string? SearchTerm { get; set; }
        public ContractStatus? Status { get; set; }
        public FinancialAgreementType? AgreementType { get; set; }
        public int? ClientId { get; set; }
        public int? CaseId { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetAllContractsQueryHandler : IRequestHandler<GetAllContractsQuery, List<ContractDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllContractsQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetAllContractsQueryHandler(
            IMapper mapper,
            ILogger<GetAllContractsQueryHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<List<ContractDto>> Handle(GetAllContractsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع العقود مع التصفية");

            // بناء التعبير للتصفية
            Expression<Func<Contract, bool>>? filter = BuildFilterExpression(request);

            var skip = (request.PageNumber - 1) * request.PageSize;

            var contracts = await _uow.Repository<Contract>()
                .GetFilteredAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.StartDate),
                    includeProperties: "Client,Case",
                    skip: skip,
                    take: request.PageSize
                );

            var contractsDto = _mapper.Map<List<ContractDto>>(contracts);

            _logger.LogInformation("تم جلب {Count} عقد بنجاح", contractsDto.Count);
            return contractsDto;
        }

        private Expression<Func<Contract, bool>>? BuildFilterExpression(GetAllContractsQuery request)
        {
            Expression<Func<Contract, bool>>? filter = null;

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                filter = c => c.ContractNumber.Contains(request.SearchTerm) ||
                             c.Title.Contains(request.SearchTerm) ||
                             c.ContractDescription.Contains(request.SearchTerm);
            }

            if (request.Status.HasValue)
            {
                filter = filter == null
                    ? c => c.Status == request.Status.Value
                    : ExpressionExtensions.And(filter, c => c.Status == request.Status.Value);
            }

            if (request.AgreementType.HasValue)
            {
                filter = filter == null
                    ? c => c.FinancialAgreementType == request.AgreementType.Value
                    : ExpressionExtensions.And(filter, c => c.FinancialAgreementType == request.AgreementType.Value);
            }

            if (request.ClientId.HasValue)
            {
                filter = filter == null
                    ? c => c.ClientId == request.ClientId.Value
                    : ExpressionExtensions.And(filter, c => c.ClientId == request.ClientId.Value);
            }

            if (request.CaseId.HasValue)
            {
                filter = filter == null
                    ? c => c.CaseId == request.CaseId.Value
                    : ExpressionExtensions.And(filter, c => c.CaseId == request.CaseId.Value);
            }

            if (request.StartDateFrom.HasValue)
            {
                filter = filter == null
                    ? c => c.StartDate >= request.StartDateFrom.Value
                    : ExpressionExtensions.And(filter, c => c.StartDate >= request.StartDateFrom.Value);
            }

            if (request.StartDateTo.HasValue)
            {
                filter = filter == null
                    ? c => c.StartDate <= request.StartDateTo.Value
                    : ExpressionExtensions.And(filter, c => c.StartDate <= request.StartDateTo.Value);
            }

            return filter;
        }
    }

    // Extension method for combining expressions
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}