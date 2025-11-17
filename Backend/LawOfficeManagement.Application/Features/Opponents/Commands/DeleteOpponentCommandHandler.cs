using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Opponents.Commands
{
    public class DeleteOpponentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteOpponentCommandHandler : IRequestHandler<DeleteOpponentCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteOpponentCommandHandler> _logger;

        public DeleteOpponentCommandHandler(IUnitOfWork uow, ILogger<DeleteOpponentCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOpponentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف الخصم: {OpponentId}", request.Id);

            var opponent = await _uow.Repository<Opponent>()
                .GetByIdAsync(request.Id);

            if (opponent == null || opponent.IsDeleted)
            {
                _logger.LogWarning("الخصم غير موجود: {OpponentId}", request.Id);
                throw new KeyNotFoundException($"الخصم بالمعرف {request.Id} غير موجود");
            }

            // التحقق من عدم وجود قضايا مرتبطة بالخصم
            var hasCases = await _uow.Repository<Core.Entities.Cases.Case>()
                .ExistsAsync(c => c.Opponents.Id == request.Id && !c.IsDeleted);

            if (hasCases)
            {
                _logger.LogWarning("لا يمكن حذف الخصم لأنه مرتبط بقضايا: {OpponentId}", request.Id);
                throw new InvalidOperationException("لا يمكن حذف الخصم لأنه مرتبط بقضايا");
            }

            opponent.IsDeleted = true;

            await _uow.Repository<Opponent>().UpdateAsync(opponent);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم حذف الخصم بنجاح: {OpponentId}", request.Id);

            return Unit.Value;
        }
    }
}
