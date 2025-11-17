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
    public class GetServiceOfficeByIdQuery : IRequest<ServiceOfficeDto>
    {
        public int Id { get; set; }
    }

    public class GetServiceOfficeByIdQueryHandler : IRequestHandler<GetServiceOfficeByIdQuery, ServiceOfficeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceOfficeByIdQueryHandler> _logger;

        public GetServiceOfficeByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetServiceOfficeByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceOfficeDto> Handle(GetServiceOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب خدمة المكتب بالمعرف: {ServiceId}", request.Id);

            var service = await _uow.Repository<ServiceOffice>()
                .FirstOrDefaultAsync(
                    s => s.Id == request.Id && !s.IsDeleted
                );

            if (service == null)
            {
                _logger.LogWarning("خدمة المكتب غير موجودة: {ServiceId}", request.Id);
                throw new KeyNotFoundException($"خدمة المكتب بالمعرف {request.Id} غير موجودة");
            }

            return _mapper.Map<ServiceOfficeDto>(service);
        }
    }
}
