using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetAllCourts
{
    public class GetAllCaseTypeQueryHandler : IRequestHandler<GetAllCaseTypeQuery, List<CourtDto>>
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public GetAllCaseTypeQueryHandler(
           IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CourtDto>> Handle(GetAllCaseTypeQuery request, CancellationToken cancellationToken)
        {
            var courts = await _uow.Repository<Court>().GetAsync(c => !c.IsDeleted);
            if (courts.Count == 0)
                return new List<CourtDto>();

            var typeIds = courts.Select(c => c.CourtTypeId).Distinct().ToList();
            var types = await _uow.Repository<CourtType>().GetAsync(t => typeIds.Contains(t.Id) && !t.IsDeleted);
            var typeDict = types.ToDictionary(t => t.Id, t => t.Name);

            var courtIds = courts.Select(c => c.Id).ToList();
            var divisions = await  _uow.Repository<CourtDivision>().GetAsync(d => courtIds.Contains(d.CourtId) && !d.IsDeleted);
            var divisionsLookup = divisions.GroupBy(d => d.CourtId).ToDictionary(g => g.Key, g => g.Select(d => _mapper.Map<CourtDivisionDto>(d)).ToList());

            var result = courts.Select(c => new CourtDto
            {
                Id = c.Id,
                Name = c.Name,
                CourtTypeId = c.CourtTypeId,
                CourtTypeName = typeDict.TryGetValue(c.CourtTypeId, out var name) ? name : string.Empty,
                Address = c.Address,
                Divisions = divisionsLookup.TryGetValue(c.Id, out var list) ? list : new List<CourtDivisionDto>()
            }).ToList();

            return result;
        }
    }
}
