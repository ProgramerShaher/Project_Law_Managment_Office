// Application/Features/Contracts/Queries/GetContractById/GetContractByIdQueryHandler.cs
using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Contracts.Queries.GetContractById
{
    public class GetContractByIdQuery : IRequest<ContractDto>
    {
        public int Id { get; set; }
    }
    public class GetContractByIdQueryHandler : IRequestHandler<GetContractByIdQuery, ContractDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetContractByIdQueryHandler> _logger;
        private readonly IUnitOfWork _uow;

        public GetContractByIdQueryHandler(
            IMapper mapper,
            ILogger<GetContractByIdQueryHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ContractDto> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جلب بيانات العقد: {ContractId}", request.Id);

            var contract = await _uow.Repository<Contract>()
                .FirstOrDefaultAsync(
                    predicate: c => c.Id == request.Id,
                    includeProperties: "Client,Case"
                );

            if (contract == null)
            {
                _logger.LogWarning("العقد غير موجود: {ContractId}", request.Id);
                throw new KeyNotFoundException($"العقد بالمعرف {request.Id} غير موجود.");
            }

            var contractDto = _mapper.Map<ContractDto>(contract);

            _logger.LogInformation("تم جلب بيانات العقد بنجاح: {ContractNumber}", contract.ContractNumber);
            return contractDto;
        }
    }

}
