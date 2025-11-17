using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Repository<Client>().GetByIdAsync(request.Id);
            if (entity == null || entity.IsDeleted)
                return null;
            return _mapper.Map<ClientDto>(entity);
        }
    }
}
