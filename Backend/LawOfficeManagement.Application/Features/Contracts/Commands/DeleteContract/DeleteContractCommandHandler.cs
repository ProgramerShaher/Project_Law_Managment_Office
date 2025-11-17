using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace LawOfficeManagement.Application.Features.Contracts.Commands.DeleteContract
{
    public class DeleteContractCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    public class DeleteContractCommandHandler : IRequestHandler<DeleteContractCommand, Unit>
    {
        private readonly ILogger<DeleteContractCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public DeleteContractCommandHandler(
            ILogger<DeleteContractCommandHandler> logger,
            IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف العقد: {ContractId}", request.Id);

            var contract = await _uow.Repository<Contract>().GetByIdAsync(request.Id);
            if (contract == null)
            {
                _logger.LogWarning("العقد غير موجود للحذف: {ContractId}", request.Id);
                throw new KeyNotFoundException($"العقد بالمعرف {request.Id} غير موجود.");
            }

            // يمكن إضافة شروط إضافية قبل الحذف
            if (contract.Status == ContractStatus.Active)
            {
                _logger.LogWarning("لا يمكن حذف عقد نشط: {ContractId}", request.Id);
                throw new InvalidOperationException("لا يمكن حذف عقد نشط. يرجى تغيير حالة العقد أولاً.");
            }

            await _uow.Repository<Contract>().DeleteAsync(contract);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف العقد بنجاح: {ContractNumber}", contract.ContractNumber);
            return Unit.Value;
        }
    }
    }