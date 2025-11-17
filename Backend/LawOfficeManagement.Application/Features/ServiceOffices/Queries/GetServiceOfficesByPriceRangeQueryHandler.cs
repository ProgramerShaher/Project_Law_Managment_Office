using AutoMapper;
using LawOfficeManagement.Application.Features.ServiceOffices.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.ServiceOffices.Queries
{
    public class GetServiceOfficesByPriceRangeQuery : IRequest<List<ServiceOfficeDto>>
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }

    public class GetServiceOfficesByPriceRangeQueryHandler : IRequestHandler<GetServiceOfficesByPriceRangeQuery, List<ServiceOfficeDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceOfficesByPriceRangeQueryHandler> _logger;

        public GetServiceOfficesByPriceRangeQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetServiceOfficesByPriceRangeQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ServiceOfficeDto>> Handle(GetServiceOfficesByPriceRangeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب خدمات المكتب بالسعر من {MinPrice} إلى {MaxPrice}", request.MinPrice, request.MaxPrice);

            var services = await _uow.Repository<ServiceOffice>()
                .GetFilteredAsync(
                    filter: s => s.ServicePrice >= request.MinPrice &&
                                s.ServicePrice <= request.MaxPrice &&
                                !s.IsDeleted,
                    orderBy: q => q.OrderBy(s => s.ServicePrice)
                );

            return _mapper.Map<List<ServiceOfficeDto>>(services);
        }
    }
}
