using AutoMapper;
using LawOfficeManagement.Application.Features.Cases.Dtos;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.UpdateCase
{
    public class UpdateCaseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateCaseDto UpdateDto { get; set; } = null!;
    }
    public class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseCommandHandler> _logger;

        public UpdateCaseCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<UpdateCaseCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث القضية {CaseId}", request.Id);

            var caseEntity = await _uow.Repository<Case>()
                .GetByIdAsync(request.Id);

            if (caseEntity == null)
                throw new InvalidOperationException("القضية غير موجودة");

            // التحقق من عدم تكرار رقم القضية (إذا تم تغييره)
            if (caseEntity.CaseNumber != request.UpdateDto.CaseNumber)
            {
                var existingCase = (await _uow.Repository<Case>()
                    .GetAllAsync())
                    .FirstOrDefault(c => c.CaseNumber == request.UpdateDto.CaseNumber && c.Id != request.Id);

                if (existingCase != null)
                    throw new InvalidOperationException("رقم القضية موجود مسبقاً");
            }

            _mapper.Map(request.UpdateDto, caseEntity);

          await  _uow.Repository<Case>().UpdateAsync(caseEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث القضية {CaseId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}