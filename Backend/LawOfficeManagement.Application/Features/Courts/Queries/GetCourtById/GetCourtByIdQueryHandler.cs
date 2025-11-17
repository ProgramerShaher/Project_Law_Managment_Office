using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Queries.GetCourtById
{
    public class GetCourtByIdQueryHandler : IRequestHandler<GetCourtByIdQuery, CourtDto?>
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public GetCourtByIdQueryHandler(
          IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CourtDto?> Handle(GetCourtByIdQuery request, CancellationToken cancellationToken)
        {
            var court = await _uow.Repository<Court>().GetByIdAsync(request.Id);
            if (court == null || court.IsDeleted)
                return null;

            var type = await _uow.Repository<CourtType>().GetByIdAsync(court.CourtTypeId);
            var divisions = await _uow.Repository<CourtDivision>().GetAsync(d => d.CourtId == court.Id && !d.IsDeleted);

            var dto = new CourtDto
            {
                Id = court.Id,
                Name = court.Name,
                CourtTypeId = court.CourtTypeId,
                CourtTypeName = type?.Name ?? string.Empty,
                Address = court.Address,
                Divisions = divisions.Select(d => _mapper.Map<CourtDivisionDto>(d)).ToList()
            };

            return dto;
        }
    }
}
