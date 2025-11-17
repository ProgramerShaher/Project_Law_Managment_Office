using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Commands.UpdateCourt
{
    public class UpdateCourtCommandHandler : IRequestHandler<UpdateCourtCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        public UpdateCourtCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
        {
            var court = await _uow.Repository<Court>().GetByIdAsync(request.Id);
            if (court == null || court.IsDeleted)
                return false;

            // Validate court type
            var courtTypeExists = await _uow.Repository<CourtType>().ExistsAsync(t => t.Id == request.CourtTypeId && !t.IsDeleted);
            if (!courtTypeExists)
                throw new InvalidOperationException("Invalid CourtType");

            // Enforce unique court name excluding current record
            var newName = request.Name.Trim();
            var nameExists = await _uow.Repository<Court>().ExistsAsync(c => !c.IsDeleted && c.Id != request.Id && c.Name == newName);
            if (nameExists)
                throw new InvalidOperationException($"المحكمة بالاسم '{newName}' موجودة بالفعل.");

            // Validate duplicate divisions in request
            if (request.Divisions != null && request.Divisions.Count > 0)
            {
                var dup = request.Divisions
                    .GroupBy(d => d.Name.Trim())
                    .FirstOrDefault(g => g.Count() > 1);
                if (dup != null)
                    throw new InvalidOperationException($"لا يمكن تكرار اسم الدائرة داخل نفس المحكمة: '{dup.Key}'");
            }

            court.Name = newName;
            court.CourtTypeId = request.CourtTypeId;
            court.Address = request.Address;

            // Soft-delete existing divisions
            var existingDivisions = await _uow.Repository<CourtDivision>().GetAsync(d => d.CourtId == court.Id && !d.IsDeleted);
            foreach (var div in existingDivisions)
            {
                div.IsDeleted = true;
                await _uow.Repository<CourtDivision>().UpdateAsync(div);
            }

            // Add new divisions
            if (request.Divisions != null && request.Divisions.Count > 0)
            {
                foreach (var d in request.Divisions)
                {
                    var newDiv = new CourtDivision { CourtId = court.Id, Name = d.Name.Trim(), JudgeName = d.JudgeName.Trim() };
                    await _uow.Repository<CourtDivision>().AddAsync(newDiv);
                }
            }

            await _uow.Repository<Court>().UpdateAsync(court);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
