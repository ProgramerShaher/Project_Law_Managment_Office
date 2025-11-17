using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.DeleteCourtType
{
    public class DeleteCourtTypeCommandHandler : IRequestHandler<DeleteCourtTypeCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteCourtTypeCommandHandler( IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteCourtTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Repository<CourtType>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                return false;

            entity.IsDeleted = true;
            await _uow.Repository<CourtType>().UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
