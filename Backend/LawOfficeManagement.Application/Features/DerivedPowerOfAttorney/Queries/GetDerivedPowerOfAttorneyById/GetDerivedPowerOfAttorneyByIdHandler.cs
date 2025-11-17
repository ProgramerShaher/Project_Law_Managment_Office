using AutoMapper;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Queries.GetDerivedPowerOfAttorneyById
{
    public class GetDerivedPowerOfAttorneyByIdHandler : IRequestHandler<GetDerivedPowerOfAttorneyByIdQuery, DerivedPowerOfAttorneyDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDerivedPowerOfAttorneyByIdHandler> _logger;

        public GetDerivedPowerOfAttorneyByIdHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetDerivedPowerOfAttorneyByIdHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DerivedPowerOfAttorneyDto> Handle(GetDerivedPowerOfAttorneyByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب الوكالة المشتقة {Id}", request.Id);

            // استخدام GetFirstOrDefaultAsync للحصول على كائن واحد فقط
            var entity = await _uow.Repository<DerivedPowerOfAttorney>()
                  .FirstOrDefaultAsync(
           p => p.Id == request.Id,
           includeProperties: "ParentPowerOfAttorney,Lawyer"
               );
            if (entity == null)
                throw new InvalidOperationException($"الوكالة المشتقة بالمعرف {request.Id} غير موجودة");

            // تحويل الكائن الفردي إلى DTO
            var result = _mapper.Map<DerivedPowerOfAttorneyDto>(entity);

            _logger.LogInformation("تم جلب الوكالة المشتقة {Id} بنجاح", request.Id);
            return result;
        }
    }
}