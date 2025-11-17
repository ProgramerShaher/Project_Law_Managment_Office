using AutoMapper;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseStage
{
    public class CreateCaseStageCommand : IRequest<int>
    {
        public CreateCaseStageDto CreateDto { get; set; } = null!;
    }
    public class CreateCaseStageCommandHandler : IRequestHandler<CreateCaseStageCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCaseStageCommandHandler> _logger;

        public CreateCaseStageCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<CreateCaseStageCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCaseStageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء مرحلة جديدة للقضية {CaseId}", request.CreateDto.CaseId);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CreateDto.CaseId);

            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            // إذا كانت هذه أول مرحلة، تأكد من أنها نشطة
            var existingStages = await _uow.Repository<CaseStage>()
                .GetAsync(cs => cs.CaseId == request.CreateDto.CaseId);

            var caseStage = _mapper.Map<CaseStage>(request.CreateDto);

            // إذا لم تكن هناك مراحل سابقة، اجعل هذه المرحلة نشطة
            if (!existingStages.Any())
            {
                caseStage.IsActive = true;
            }
            else
            {
                // إذا كانت هناك مراحل سابقة، اجعل هذه غير نشطة
                caseStage.IsActive = false;
            }

            await _uow.Repository<CaseStage>().AddAsync(caseStage);

            _logger.LogInformation("تم إنشاء المرحلة بنجاح بالمعرف {StageId} للقضية {CaseId}",
                caseStage.Id, request.CreateDto.CaseId);

            return caseStage.Id;
        }
    }
}