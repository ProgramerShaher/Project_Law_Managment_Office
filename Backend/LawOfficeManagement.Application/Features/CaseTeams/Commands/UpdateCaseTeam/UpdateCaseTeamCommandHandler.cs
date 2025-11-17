using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.UpdateCaseTeam
{
    public class UpdateCaseTeamCommandHandler : IRequestHandler<UpdateCaseTeamCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseTeamCommandHandler> _logger;

        public UpdateCaseTeamCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<UpdateCaseTeamCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCaseTeamCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث فريق العمل {TeamId}", request.Id);

            var caseTeam = await _uow.Repository<CaseTeam>()
                .GetByIdAsync(request.Id);

            if (caseTeam == null)
                throw new InvalidOperationException("سجل فريق العمل غير موجود");


            _mapper.Map(request.UpdateDto, caseTeam);

            await _uow.Repository<CaseTeam>().UpdateAsync(caseTeam);

            _logger.LogInformation("تم تحديث فريق العمل {TeamId} بنجاح", request.Id);

            return Unit.Value;
        }
    }
}