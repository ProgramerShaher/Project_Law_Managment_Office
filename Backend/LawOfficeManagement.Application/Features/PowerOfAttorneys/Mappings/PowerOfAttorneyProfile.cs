using AutoMapper;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.CreatePowerOfAttorney;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Mappings
{
    public class PowerOfAttorneyProfile : Profile
    {
        public PowerOfAttorneyProfile()
        {
            CreateMap<CreatePowerOfAttorneyCommand, PowerOfAttorney>();
            CreateMap<PowerOfAttorney, PowerOfAttorneyDto>();

            CreateMap<PowerOfAttorney, PowerOfAttorneyDto>()
    //.ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
    .ForMember(dest => dest.OfficeName, opt => opt.MapFrom(src => src.Office != null ? src.Office.OfficeName : null))
    .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer != null ? src.Lawyer.FullName : null))
    .ReverseMap();
        }
    }
}
