// LawOfficeManagement.Application.Features.LegalConsultations.Mappings
using AutoMapper;
using LawOfficeManagement.Application.Features.LegalConsultations.DTOs;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.LegalConsultations.Mappings
{
    public class LegalConsultationMappingProfile : Profile
    {
        public LegalConsultationMappingProfile()
        {
            // Create
            CreateMap<CreateLegalConsultationDto, LegalConsultation>();

            // Update
            CreateMap<UpdateLegalConsultationDto, LegalConsultation>();

            // Entity to DTO
            CreateMap<LegalConsultation, LegalConsultationDto>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.FullName))
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.ServiceOffice.ServiceName))
                .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.ServiceOffice.ServicePrice));
        }
    }
}