// LawOfficeManagement.Application.Features.ServiceOffices.Queries
using AutoMapper;
using LawOfficeManagement.Application.Features.ServiceOffices.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.ServiceOffices.Queries
{
    // Get All Service Offices
    public class GetAllServiceOfficesQuery : IRequest<List<ServiceOfficeDto>>
    {
        public bool IncludeInactive { get; set; } = false;
    }

    public class GetAllServiceOfficesQueryHandler : IRequestHandler<GetAllServiceOfficesQuery, List<ServiceOfficeDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllServiceOfficesQueryHandler> _logger;

        public GetAllServiceOfficesQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllServiceOfficesQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ServiceOfficeDto>> Handle(GetAllServiceOfficesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب جميع خدمات المكتب");

            var services = await _uow.Repository<ServiceOffice>()
                .GetFilteredAsync(
                    filter: request.IncludeInactive ? null : s => !s.IsDeleted,
                    orderBy: q => q.OrderBy(s => s.ServiceName)
                );

            return _mapper.Map<List<ServiceOfficeDto>>(services);
        }
    }

    
  
}