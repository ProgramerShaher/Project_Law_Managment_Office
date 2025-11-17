using AutoMapper;
using LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.Mapping
{
    public class DerivedPowerOfAttorneyProfile : Profile
    {
        public DerivedPowerOfAttorneyProfile()
        {
            // Mapping للإنشاء
            CreateMap<CreateDerivedPowerOfAttorneyDto, DerivedPowerOfAttorney>()
                .ForMember(dest => dest.Derived_Document_Agent_Url, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ParentPowerOfAttorney, opt => opt.Ignore())
                .ForMember(dest => dest.Lawyer, opt => opt.Ignore());

            // Mapping للقراءة
            CreateMap<DerivedPowerOfAttorney, DerivedPowerOfAttorneyDto>()
                .ForMember(dest => dest.ParentPowerOfAttorneyNumber,
                    opt => opt.MapFrom(src => src.ParentPowerOfAttorney != null ? src.ParentPowerOfAttorney.AgencyNumber : ""))
                .ForMember(dest => dest.LawyerName,
                    opt => opt.MapFrom(src => src.Lawyer != null ? $"{src.Lawyer.FullName}" : ""));

            // Mapping للتحديث
            CreateMap<CreateDerivedPowerOfAttorneyDto, DerivedPowerOfAttorney>()
                .ForMember(dest => dest.Derived_Document_Agent_Url, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ParentPowerOfAttorney, opt => opt.Ignore())
                .ForMember(dest => dest.Lawyer, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}