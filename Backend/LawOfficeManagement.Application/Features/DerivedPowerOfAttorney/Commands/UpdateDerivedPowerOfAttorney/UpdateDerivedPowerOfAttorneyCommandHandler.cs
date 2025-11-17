using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Commands.UpdateDerivedPowerOfAttorney
{
    public class UpdateDerivedPowerOfAttorneyCommandHandler : IRequestHandler<UpdateDerivedPowerOfAttorneyCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDerivedPowerOfAttorneyCommandHandler> _logger;

        public UpdateDerivedPowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<UpdateDerivedPowerOfAttorneyCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateDerivedPowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث الوكالة المشتقة {Id}", request.Id);

            var entity = await _uow.Repository<DerivedPowerOfAttorney>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                throw new InvalidOperationException("الوكالة المشتقة غير موجودة أو محذوفة");

            // التحقق من وجود الوكالة الأصلية إذا تم تغييرها
            if (request.UpdateDto.ParentPowerOfAttorneyId != entity.ParentPowerOfAttorneyId)
            {
                var parentPowerOfAttorney = await _uow.Repository<PowerOfAttorney>()
                    .GetByIdAsync(request.UpdateDto.ParentPowerOfAttorneyId);

                if (parentPowerOfAttorney == null || parentPowerOfAttorney.IsDeleted)
                    throw new InvalidOperationException("الوكالة الأصلية غير موجودة أو محذوفة");
            }

            // التحقق من وجود المحامي إذا تم تغييره
            if (request.UpdateDto.LawyerId != entity.LawyerId)
            {
                var lawyer = await _uow.Repository<Lawyer>()
                    .GetByIdAsync(request.UpdateDto.LawyerId);

                if (lawyer == null || lawyer.IsDeleted)
                    throw new InvalidOperationException("المحامي غير موجود أو محذوف");
            }

            _mapper.Map(request.UpdateDto, entity);

            // تحديث الملف إذا تم رفع ملف جديد
         

            await  _uow.Repository<DerivedPowerOfAttorney>().UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث الوكالة المشتقة {Id} بنجاح", request.Id);
            return Unit.Value;
        }
    }
}