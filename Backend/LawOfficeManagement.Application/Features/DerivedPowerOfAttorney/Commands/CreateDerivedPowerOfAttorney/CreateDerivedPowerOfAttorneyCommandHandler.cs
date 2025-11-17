using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.CreateDerivedPowerOfAttorney
{
    public class CreateDerivedPowerOfAttorneyCommandHandler : IRequestHandler<CreateDerivedPowerOfAttorneyCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDerivedPowerOfAttorneyCommandHandler> _logger;

        public CreateDerivedPowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<CreateDerivedPowerOfAttorneyCommandHandler> logger
            )
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateDerivedPowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء وكالة مشتقة جديدة للوكالة الأصلية {ParentPowerOfAttorneyId}",
                request.CreateDto.ParentPowerOfAttorneyId);

            // التحقق من وجود الوكالة الأصلية
            var parentPowerOfAttorney = await _uow.Repository<PowerOfAttorney>()
                .GetByIdAsync(request.CreateDto.ParentPowerOfAttorneyId);

            if (parentPowerOfAttorney == null || parentPowerOfAttorney.IsDeleted)
                throw new InvalidOperationException("الوكالة الأصلية غير موجودة أو محذوفة");

            // التحقق من وجود المحامي
            var lawyer = await _uow.Repository<Lawyer>()
                .GetByIdAsync(request.CreateDto.LawyerId);

            if (lawyer == null || lawyer.IsDeleted)
                throw new InvalidOperationException("المحامي غير موجود أو محذوف");

            var entity = _mapper.Map<DerivedPowerOfAttorney>(request.CreateDto);

           
            if (entity.ParentPowerOfAttorney.DerivedPowerOfAttorney == true)
            {
                await _uow.Repository<DerivedPowerOfAttorney>().AddAsync(entity);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("تم إنشاء الوكالة المشتقة رقم {DerivedNumber} بنجاح بالمعرف {Id}",
                    entity.DerivedNumber, entity.Id);

                return entity.Id;

            }
            else _logger.LogInformation("لا يمكن اشتقاق وكالة من الوكالة التي تم اختيارها  ");
            throw new Exception("لا يمكن اشتقاق وكالة من الوكالة التي تم اختيارها ");






        }
    }
}