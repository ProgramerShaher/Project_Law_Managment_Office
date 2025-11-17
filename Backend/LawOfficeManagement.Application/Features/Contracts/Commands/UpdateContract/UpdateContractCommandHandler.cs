using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Contracts.Commands.UpdateContract
{
    public class UpdateContractCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateContractDto ContractDto { get; set; } = null!;
    }
    public class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateContractCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateContractCommandHandler(
            IMapper mapper,
            ILogger<UpdateContractCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث العقد: {ContractId}", request.Id);

            var contract = await _uow.Repository<Contract>().GetByIdAsync(request.Id);
            if (contract == null)
            {
                _logger.LogWarning("العقد غير موجود للتحديث: {ContractId}", request.Id);
                throw new KeyNotFoundException($"العقد بالمعرف {request.Id} غير موجود.");
            }

            // التحقق من صحة البيانات المالية
            ValidateFinancialData(request.ContractDto);

            // تحديث الخصائص
            _mapper.Map(request.ContractDto, contract);

            await   _uow.Repository<Contract>().UpdateAsync(contract);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث العقد بنجاح: {ContractNumber}", contract.ContractNumber);
            return Unit.Value;
        }

        private void ValidateFinancialData(UpdateContractDto dto)
        {
            switch (dto.FinancialAgreementType)
            {
                case FinancialAgreementType.PercentageBased:
                    if (!dto.TotalCaseAmount.HasValue || !dto.Percentage.HasValue)
                        throw new InvalidOperationException("نوع الاتفاق بالنسبة يتطلب تحديد المبلغ الكلي والنسبة المئوية.");
                    break;

                case FinancialAgreementType.FixedAmount:
                case FinancialAgreementType.ServiceFees:
                    if (!dto.FinalAgreedAmount.HasValue)
                        throw new InvalidOperationException("نوع الاتفاق يتطلب تحديد المبلغ النهائي.");
                    break;
            }
        }
    }
}
