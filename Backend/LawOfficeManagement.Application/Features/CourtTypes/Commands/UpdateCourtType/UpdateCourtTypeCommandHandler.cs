using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.UpdateCourtType
{
    public class UpdateCourtTypeCommandHandler : IRequestHandler<UpdateCourtTypeCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public UpdateCourtTypeCommandHandler( IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateCourtTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Repository<CourtType>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                return false;

            var dup = await _uow.Repository<CourtType>().ExistsAsync(x => x.Id != request.Id && x.Name == request.Name && !x.IsDeleted);
            if (dup) throw new InvalidOperationException($"Court type '{request.Name}' already exists");

            entity.Name = request.Name.Trim();
            entity.Notes = request.Notes;
            await _uow.Repository<CourtType>().UpdateAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
