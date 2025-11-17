using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Commands.CreateCaseType
{
 
    public class CreateCaseTypeCommandHandler : IRequestHandler<CreateCaseTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCaseTypeCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public CreateCaseTypeCommandHandler(
            IMapper mapper,
            ILogger<CreateCaseTypeCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(CreateCaseTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية إنشاء نوع قضية جديد: {CaseTypeName}", request.Name);

            // التحقق من عدم وجود نوع قضية بنفس الاسم
            var nameExists = await _uow.Repository<CaseType>().ExistsAsync(ct =>
                ct.Name == request.Name);

            if (nameExists)
            {
                _logger.LogWarning("فشلت محاولة إنشاء نوع قضية باسم موجود بالفعل: {Name}", request.Name);
                throw new InvalidOperationException($"نوع القضية '{request.Name}' موجود بالفعل.");
            }

            // تحويل إلى كيان
            var caseTypeEntity = _mapper.Map<CaseType>(request);

            // إضافة إلى قاعدة البيانات
            await _uow.Repository<CaseType>().AddAsync(caseTypeEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء نوع القضية {CaseTypeName} بنجاح بالمعرف: {CaseTypeId}",
                caseTypeEntity.Name, caseTypeEntity.Id);

            return caseTypeEntity.Id;
        }
    }
}