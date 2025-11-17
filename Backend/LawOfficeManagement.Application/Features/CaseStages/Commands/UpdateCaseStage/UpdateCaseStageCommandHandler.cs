using AutoMapper;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.UpdateCaseStage
{
    public class UpdateCaseStageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateCaseStageDto UpdateDto { get; set; } = null!;
    }

    public class UpdateCaseStageDto
    {
        public string Stage { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? EndDateStage { get; set; }
    }
    public class UpdateCaseStageCommandHandler : IRequestHandler<UpdateCaseStageCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseStageCommandHandler> _logger;

        public UpdateCaseStageCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<UpdateCaseStageCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCaseStageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث المرحلة {StageId}", request.Id);

            var caseStage = await _uow.Repository<CaseStage>()
                .GetByIdAsync(request.Id);

            if (caseStage == null)
                throw new InvalidOperationException("المرحلة غير موجودة");

            _mapper.Map(request.UpdateDto, caseStage);

            await _uow.Repository<CaseStage>().UpdateAsync(caseStage);

            _logger.LogInformation("تم تحديث المرحلة {StageId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}