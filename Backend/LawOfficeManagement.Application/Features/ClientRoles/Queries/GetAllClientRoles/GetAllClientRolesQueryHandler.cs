using AutoMapper;
using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;

namespace LawOfficeManagement.Application.Features.ClientRoles.Queries.GetAllClientRoles
{
    public class GetAllClientRolesQueryHandler : IRequestHandler<GetAllClientRolesQuery, List<ClientRoleDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllClientRolesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ClientRoleDto>> Handle(GetAllClientRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _uow.Repository<ClientRole>().GetAllAsync();
            var active = roles.Where(r => !r.IsDeleted).ToList();
            return _mapper.Map<List<ClientRoleDto>>(active);
        }
    }
}
