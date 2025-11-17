using AutoMapper;
using LawOfficeManagement.Application.Features.Clients.Commands.CreateClient;
using LawOfficeManagement.Application.Features.Clients.Queries.GetAllClients; // <-- إضافة هذا
using LawOfficeManagement.Application.Features.Clients.Queries.GetClientById;
using LawOfficeManagement.Core.Common;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.Clients.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
           
            CreateMap<CreateClientCommand, Client>();
               
            CreateMap<Client, ClientSummaryDto>()
                .ForMember(dest => dest.ClientTypeName,
                           opt => opt.MapFrom(src => src.ClientType.ToString())) // تحويل الـ enum إلى نص
                .ForMember(dest => dest.ClientRoleName,
                           opt => opt.MapFrom(src => src.ClientRole.Name)); // جلب الاسم من الكيان المرتبط

            CreateMap<Client, ClientDto>().ReverseMap();
                //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                //.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                //.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                //.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));
        }
    }
}
