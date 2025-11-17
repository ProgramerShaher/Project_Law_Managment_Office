using AutoMapper;
using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Queries.GetAllCourtTypes
{
    public class GetAllCourtTypesQueryHandler : IRequestHandler<GetAllCourtTypesQuery, List<CourtTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;


        public GetAllCourtTypesQueryHandler(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<List<CourtTypeDto>> Handle(GetAllCourtTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.Repository<CourtType>().GetAsync(x => !x.IsDeleted);
            return _mapper.Map<List<CourtTypeDto>>(entities);
        }
    }
}
