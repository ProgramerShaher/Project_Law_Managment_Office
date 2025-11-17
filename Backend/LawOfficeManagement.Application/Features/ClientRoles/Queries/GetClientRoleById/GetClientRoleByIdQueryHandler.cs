using AutoMapper;
using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Queries.GetClientRoleById
{
    public class GetClientRoleByIdQueryHandler : IRequestHandler<GetClientRoleByIdQuery, ClientRoleDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetClientRoleByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ClientRoleDto?> Handle(GetClientRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _uow.Repository<ClientRole>().GetByIdAsync(request.Id);
            if (role == null || role.IsDeleted)
                return null;
            return _mapper.Map<ClientRoleDto>(role);
        }
    }
}
