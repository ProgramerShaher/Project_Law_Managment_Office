using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.DTOs;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Features.Courts.Mappings
{
    public class CourtProfile : Profile
    {
        public CourtProfile()
        {
            CreateMap<CourtDivision, CourtDivisionDto>();
            CreateMap<Court, CourtDto>()
                .ForMember(d => d.CourtTypeName, opt => opt.MapFrom(s => s.CourtType.Name));
            // Mapping ··„Õ«ﬂ„
            CreateMap<Court, CourtDto>()
                .ForMember(dest => dest.CourtTypeName,
                    opt => opt.MapFrom(src => src.CourtType != null ? src.CourtType.Name : ""));

            // Mapping ·√ﬁ”«„ «·„Õﬂ„…
            CreateMap<CourtDivision, CourtDivisionDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Court != null ? src.Court.Name : ""));
         //     CreateMap<CourtDivision, CourtDivisionDto>();

        }

    }
}
