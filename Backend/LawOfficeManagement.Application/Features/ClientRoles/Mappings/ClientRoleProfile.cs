using AutoMapper;
using LawOfficeManagement.Application.DTOs;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.ClientRoles.Mappings
{
    public class ClientRoleProfile : Profile
    {
        public ClientRoleProfile()
        {
            CreateMap<ClientRole, ClientRoleDto>();
        }
    }
}
