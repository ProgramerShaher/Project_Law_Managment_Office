using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.CreateCourtType
{
    public class CreateCourtTypeCommandHandler : IRequestHandler<CreateCourtTypeCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CreateCourtTypeCommandHandler( IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Handle(CreateCourtTypeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");

            var exists = await _uow.Repository<CourtType>().ExistsAsync(x => x.Name == request.Name && !x.IsDeleted);
            if (exists) throw new InvalidOperationException($"Court type '{request.Name}' already exists");

            var entity = new CourtType { Name = request.Name.Trim(), Notes = request.Notes };
            await _uow.Repository<CourtType>().AddAsync(entity);
            await _uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
