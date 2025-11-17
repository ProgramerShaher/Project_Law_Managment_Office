using AutoMapper;
using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Queries.GetCourtTypeById
{
    public class GetCourtTypeByIdQueryHandler : IRequestHandler<GetCourtTypeByIdQuery, CourtTypeDto?>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GetCourtTypeByIdQueryHandler(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<CourtTypeDto?> Handle(GetCourtTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Repository<CourtType>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                return null;

            return _mapper.Map<CourtTypeDto>(entity);
        }
    }
}
