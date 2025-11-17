using AutoMapper;
using LawOfficeManagement.Application.Features.Cases.Dtos;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.CreateCase
{
     public class CreateCaseCommand : IRequest<int>
    {
        public CreateCaseDto CreateDto { get; set; } = null!;
    }

    public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCaseCommandHandler> _logger;

        public CreateCaseCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<CreateCaseCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء قضية جديدة: {Title}", request.CreateDto.Title);

            // التحقق من وجود العميل
            var client = await _uow.Repository<Client>()
                .GetByIdAsync(request.CreateDto.ClientId);
            if (client == null || client.IsDeleted)
                throw new InvalidOperationException("العميل غير موجود أو محذوف");

            // التحقق من وجود المحكمة
            var court = await _uow.Repository<Court>()
                .GetByIdAsync(request.CreateDto.CourtId);
            if (court == null || court.IsDeleted)
                throw new InvalidOperationException("المحكمة غير موجودة أو محذوفة");

            // التحقق من وجود الخصم
            var opponent = await _uow.Repository<Opponent>()
                .GetByIdAsync(request.CreateDto.OpponentId);
            if (opponent == null || opponent.IsDeleted)
                throw new InvalidOperationException("الخصم غير موجود أو محذوف");

            // التحقق من عدم تكرار رقم القضية
            var existingCase = (await _uow.Repository<Case>()
                .GetAllAsync())
                .FirstOrDefault(c => c.CaseNumber == request.CreateDto.CaseNumber);

            if (existingCase != null)
                throw new InvalidOperationException("رقم القضية موجود مسبقاً");

            var caseEntity = _mapper.Map<Case>(request.CreateDto);

            await _uow.Repository<Case>().AddAsync(caseEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء القضية بنجاح بالمعرف {CaseId}", caseEntity.Id);

            return caseEntity.Id;
        }
    }
}