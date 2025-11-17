
// Application/Features/Contracts/Commands/CreateContract/CreateContractCommandHandler.cs
using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Contracts.Commands.CreateContract
{
    public class CreateContractCommand : IRequest<int>
    {
        public CreateContractDto ContractDto { get; set; } = null!;
    }
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateContractCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public CreateContractCommandHandler(
            IMapper mapper,
            ILogger<CreateContractCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية إنشاء عقد جديد: {ContractNumber}", request.ContractDto.ContractNumber);

            // التحقق من رقم العقد الفريد
            var contractNumberExists = await _uow.Repository<Contract>().ExistsAsync(c =>
                c.ContractNumber == request.ContractDto.ContractNumber);

            if (contractNumberExists)
            {
                _logger.LogWarning("فشلت محاولة إنشاء عقد برقم موجود بالفعل: {ContractNumber}", request.ContractDto.ContractNumber);
                throw new InvalidOperationException($"رقم العقد '{request.ContractDto.ContractNumber}' مستخدم بالفعل.");
            }

            // التحقق من وجود العميل
            var clientExists = await _uow.Repository<Client>().ExistsAsync(c =>
                c.Id == request.ContractDto.ClientId && !c.IsDeleted);

            if (!clientExists)
            {
                _logger.LogWarning("العميل غير موجود: {ClientId}", request.ContractDto.ClientId);
                throw new InvalidOperationException("العميل المحدد غير موجود.");
            }

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>().ExistsAsync(c =>
                c.Id == request.ContractDto.CaseId && !c.IsDeleted);

            if (!caseExists)
            {
                _logger.LogWarning("القضية غير موجودة: {CaseId}", request.ContractDto.CaseId);
                throw new InvalidOperationException("القضية المحددة غير موجودة.");
            }

            // التحقق من عدم وجود عقد نشط لنفس القضية
            var existingActiveContract = await _uow.Repository<Contract>().ExistsAsync(c =>
                c.CaseId == request.ContractDto.CaseId &&
                c.Status == ContractStatus.Active);

            if (existingActiveContract)
            {
                _logger.LogWarning("يوجد عقد نشط بالفعل للقضية: {CaseId}", request.ContractDto.CaseId);
                throw new InvalidOperationException("يوجد عقد نشط بالفعل لهذه القضية.");
            }

            // التحقق من صحة البيانات المالية بناءً على نوع الاتفاق
            ValidateFinancialData(request.ContractDto);

            // Mapping إلى الكيان
            var contractEntity = _mapper.Map<Contract>(request.ContractDto);

            await _uow.Repository<Contract>().AddAsync(contractEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء العقد {ContractNumber} بنجاح بالمعرف: {ContractId}",
                contractEntity.ContractNumber, contractEntity.Id);

            return contractEntity.Id;
        }

        private void ValidateFinancialData(CreateContractDto dto)
        {
            switch (dto.FinancialAgreementType)
            {
                case FinancialAgreementType.PercentageBased:
                    if (!dto.TotalCaseAmount.HasValue || !dto.Percentage.HasValue)
                        throw new InvalidOperationException("نوع الاتفاق بالنسبة يتطلب تحديد المبلغ الكلي والنسبة المئوية.");
                    if (dto.Percentage.Value <= 0 || dto.Percentage.Value > 100)
                        throw new InvalidOperationException("النسبة المئوية يجب أن تكون بين 1 و 100.");
                    break;

                case FinancialAgreementType.FixedAmount:
                    if (!dto.FinalAgreedAmount.HasValue)
                        throw new InvalidOperationException("نوع الاتفاق بالمبلغ الثابت يتطلب تحديد المبلغ النهائي.");
                    break;

                case FinancialAgreementType.ServiceFees:
                    if (!dto.FinalAgreedAmount.HasValue)
                        throw new InvalidOperationException("نوع الاتفاق بالأتعاب يتطلب تحديد مبلغ الأتعاب.");
                    break;
            }
        }
    }
}