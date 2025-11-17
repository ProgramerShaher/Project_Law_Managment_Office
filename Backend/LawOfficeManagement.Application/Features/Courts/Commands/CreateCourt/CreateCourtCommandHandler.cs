using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using System.Diagnostics.Metrics;

namespace LawOfficeManagement.Application.Features.Courts.Commands.CreateCourt
{
    public class CreateCaseTypeCommandHandler : IRequestHandler<CreateCaseTypeCommand, int>
    {

        //private readonly IRepository<Court> _courtRepo;
        //private readonly IRepository<CourtType> _courtTypeRepo;
        private readonly IUnitOfWork _uow;

        public CreateCaseTypeCommandHandler(
           
            IUnitOfWork uow)
        {
          
            _uow = uow;
        }

        public async Task<int> Handle(CreateCaseTypeCommand request, CancellationToken cancellationToken)
        {
            var type = await _uow.Repository<CourtType>().GetByIdAsync(request.CourtTypeId);
            if (type == null || type.IsDeleted)
                throw new InvalidOperationException("Invalid CourtType");

            var court = new Court
            {
                Name = request.Name.Trim(),
                CourtTypeId = request.CourtTypeId,
                Address = request.Address
            };

            if (request.Divisions?.Count > 0)
            {
                foreach (var d in request.Divisions)
                {
                    court.Divisions.Add(new CourtDivision { Name = d.Name.Trim(), JudgeName = d.JudgeName.Trim() });
                }
            }

            await _uow.Repository<Court>().AddAsync(court);
            await _uow.SaveChangesAsync(cancellationToken);
            return court.Id;
        }
    }
}
