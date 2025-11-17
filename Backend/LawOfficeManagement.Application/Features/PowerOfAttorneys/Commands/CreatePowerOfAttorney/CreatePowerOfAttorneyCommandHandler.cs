using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.CreatePowerOfAttorney
{
    public class CreatePowerOfAttorneyCommandHandler : IRequestHandler<CreatePowerOfAttorneyCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePowerOfAttorneyCommandHandler> _logger;

        public CreatePowerOfAttorneyCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<CreatePowerOfAttorneyCommandHandler> logger
           )
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreatePowerOfAttorneyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء وكالة جديدة للعميل {ClientId}", request.ClientId);

            // تحقق أن أحدهما فقط (المكتب أو المحامي) محدد
            if (request.OfficeID.HasValue && request.LawyerID.HasValue)
                throw new InvalidOperationException("لا يمكن تحديد كل من المكتب والمحامي في نفس الوقت.");

            if (!request.OfficeID.HasValue && !request.LawyerID.HasValue)
                throw new InvalidOperationException("يجب تحديد مكتب أو محامي واحد على الأقل.");

     
                
            var entity = _mapper.Map<PowerOfAttorney>(request);

            // حفظ صورة الوكالة
            
          
           
            await _uow.Repository<PowerOfAttorney>().AddAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء الوكالة رقم {AgencyNumber} بنجاح بالمعرف {Id}", entity.AgencyNumber, entity.Id);
            return entity.Id;
        }
    }
}
