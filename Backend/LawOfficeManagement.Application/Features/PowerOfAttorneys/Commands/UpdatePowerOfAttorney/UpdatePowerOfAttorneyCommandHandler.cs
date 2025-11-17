using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.UpdatePowerOfAttorney
{
    public class UpdatePowerOfAttorneyCommandHandler : IRequestHandler<UpdatePowerOfAttorneyCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdatePowerOfAttorneyCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdatePowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            ILogger<UpdatePowerOfAttorneyCommandHandler> logger,
            IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث الوكالة رقم {Id}", request.Id);

            var repo = _uow.Repository<PowerOfAttorney>();
            var entity = await repo.GetByIdAsync(request.Id);
            if (entity == null)
                throw new KeyNotFoundException($"الوكالة بالمعرف {request.Id} غير موجودة.");

            // تحديث البيانات الأساسية
            entity.AgencyNumber = request.AgencyNumber;
            entity.IssueDate = request.IssueDate;
            entity.ExpiryDate = request.ExpiryDate;
            entity.IssuingAuthority = request.IssuingAuthority;
            entity.ClientId = request.ClientId;
            entity.OfficeID = request.OfficeID;
            entity.LawyerID = request.LawyerID;
            entity.AgencyType = request.AgencyType;
            entity.DerivedPowerOfAttorney = request.DerivedPowerOfAttorney;

          

         await   repo.UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

             _logger.LogInformation("تم تحديث الوكالة رقم {AgencyNumber} بنجاح", entity.AgencyNumber);
            return true;
        }
    }
}
