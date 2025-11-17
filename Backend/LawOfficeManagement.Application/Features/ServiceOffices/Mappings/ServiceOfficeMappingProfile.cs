// LawOfficeManagement.Application.Features.ServiceOffices.Mappings
using AutoMapper;
using LawOfficeManagement.Application.Features.ServiceOffices.DTOs;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Features.ServiceOffices.Mappings
{
    public class ServiceOfficeMappingProfile : Profile
    {
        public ServiceOfficeMappingProfile()
        {
            // Create
            CreateMap<CreateServiceOfficeDto, ServiceOffice>();

            // Update
            CreateMap<UpdateServiceOfficeDto, ServiceOffice>();

            // Entity to DTO
            CreateMap<ServiceOffice, ServiceOfficeDto>()
                .ForMember(dest => dest.ConsultationCount, opt => opt.MapFrom(src => src.legalConsultations.Count));
        }
    }
}