//using LawOfficeManagement.Core.Entities.Cases;
//using LawOfficeManagement.Core.Interfaces;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.DeleteCaseTeam
//{
//    public class DeleteCaseTeamCommandHanxldler : IRequestHandler<DeleteCaseTeamCommand, Unit>
//    {
//        private readonly IUnitOfWork _uow;
//        private readonly ILogger<DeleteCaseTeamCommandHandler> _logger;

//        public DeleteCaseTeamCommandHandler(
//            IUnitOfWork uow,
//            ILogger<DeleteCaseTeamCommandHandler> logger)
//        {
//            _uow = uow;
//            _logger = logger;
//        }

//        public async Task<Unit> Handle(DeleteCaseTeamCommand request, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("بدء حذف فريق العمل {TeamId}", request.Id);

//            var caseTeam = await _uow.Repository<CaseTeam>()
//                .GetByIdAsync(request.Id);

//            if (caseTeam == null)
//                throw new InvalidOperationException("سجل فريق العمل غير موجود");

//            // التحقق من عدم وجود مهام مرتبطة
//            var hasTasks = await _uow.Repository<TaskItem>()
//                .ExistsAsync(t => t.CaseTeamId == request.Id);

//            if (hasTasks)
//                throw new InvalidOperationException("لا يمكن حذف فريق العمل لأنه يحتوي على مهام مرتبطة");

//            await _uow.Repository<CaseTeam>().DeleteAsync(caseTeam);

//            _logger.LogInformation("تم حذف فريق العمل {TeamId} بنجاح", request.Id);

//            return Unit.Value;
//        }
//    }
//}