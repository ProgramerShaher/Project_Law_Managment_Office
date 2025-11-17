using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Commands.DeleteCourt
{
    public class DeleteCourtCommandHandler : IRequestHandler<DeleteCourtCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteCourtCommandHandler(IRepository<Court> repo, IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Repository<Court>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                return false;

            entity.IsDeleted = true;
            await _uow.Repository<Court>().UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
