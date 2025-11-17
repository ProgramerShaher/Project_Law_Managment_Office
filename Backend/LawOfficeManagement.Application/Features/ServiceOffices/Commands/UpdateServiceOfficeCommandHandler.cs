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

namespace LawOfficeManagement.Application.Features.ServiceOffices.Commands
{
    public class UpdateServiceOfficeCommand : IRequest<Unit>
    {
        public UpdateServiceOfficeDto UpdateDto { get; set; }
    }

    public class UpdateServiceOfficeCommandHandler : IRequestHandler<UpdateServiceOfficeCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateServiceOfficeCommandHandler> _logger;

        public UpdateServiceOfficeCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateServiceOfficeCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateServiceOfficeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث خدمة المكتب: {ServiceId}", request.UpdateDto.Id);

            var service = await _uow.Repository<ServiceOffice>()
                .GetByIdAsync(request.UpdateDto.Id);

            if (service == null || service.IsDeleted)
            {
                _logger.LogWarning("خدمة المكتب غير موجودة: {ServiceId}", request.UpdateDto.Id);
                throw new KeyNotFoundException($"خدمة المكتب بالمعرف {request.UpdateDto.Id} غير موجودة");
            }

            // التحقق من عدم وجود خدمة أخرى بنفس الاسم
            if (service.ServiceName != request.UpdateDto.ServiceName)
            {
                var nameExists = await _uow.Repository<ServiceOffice>()
                    .ExistsAsync(s => s.ServiceName == request.UpdateDto.ServiceName &&
                                     s.Id != request.UpdateDto.Id &&
                                     !s.IsDeleted);

                if (nameExists)
                {
                    _logger.LogWarning("اسم الخدمة مستخدم مسبقًا: {ServiceName}", request.UpdateDto.ServiceName);
                    throw new InvalidOperationException($"الخدمة باسم '{request.UpdateDto.ServiceName}' موجودة مسبقًا");
                }
            }

            _mapper.Map(request.UpdateDto, service);

            await _uow.Repository<ServiceOffice>().UpdateAsync(service);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث خدمة المكتب بنجاح: {ServiceId}", service.Id);

            return Unit.Value;
        }
    }
}
