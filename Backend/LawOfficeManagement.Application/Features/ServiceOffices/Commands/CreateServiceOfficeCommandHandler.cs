// LawOfficeManagement.Application.Features.ServiceOffices.Commands
using AutoMapper;
using LawOfficeManagement.Application.Features.ServiceOffices.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.ServiceOffices.Commands
{
    // Create Service Office
    public class CreateServiceOfficeCommand : IRequest<int>
    {
        public CreateServiceOfficeDto CreateDto { get; set; }
    }

    public class CreateServiceOfficeCommandHandler : IRequestHandler<CreateServiceOfficeCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateServiceOfficeCommandHandler> _logger;

        public CreateServiceOfficeCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateServiceOfficeCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateServiceOfficeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء خدمة مكتب جديدة: {ServiceName}", request.CreateDto.ServiceName);

            // التحقق من عدم وجود خدمة بنفس الاسم
            var serviceExists = await _uow.Repository<ServiceOffice>()
                .ExistsAsync(s => s.ServiceName == request.CreateDto.ServiceName && !s.IsDeleted);

            if (serviceExists)
            {
                _logger.LogWarning("محاولة إنشاء خدمة باسم موجود مسبقًا: {ServiceName}", request.CreateDto.ServiceName);
                throw new InvalidOperationException($"الخدمة باسم '{request.CreateDto.ServiceName}' موجودة مسبقًا");
            }

            var service = _mapper.Map<ServiceOffice>(request.CreateDto);

            await _uow.Repository<ServiceOffice>().AddAsync(service);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء خدمة المكتب بنجاح بالمعرف: {ServiceId}", service.Id);

            return service.Id;
        }
    }

    // Update Service Office
  

    // Delete Service Office
   
}