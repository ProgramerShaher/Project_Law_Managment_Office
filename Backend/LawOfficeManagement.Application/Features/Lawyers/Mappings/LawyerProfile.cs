using AutoMapper;
using LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer;
using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using LawOfficeManagement.Core.Entities;

public class LawyerProfile : Profile
{
    public LawyerProfile()
    {
        CreateMap<Lawyer, LawyerDto>().ReverseMap();

        CreateMap<CreateLawyerCommand, Lawyer>()
            // تعيين mapping مخصص للحقول المختلفة
            .ForMember(dest => dest.QualificationDocumentsPath,
                       opt => opt.MapFrom(src => src.QualificationDocumentsPath ?? string.Empty))
            .ForMember(dest => dest.IdentityImagePath,
                       opt => opt.MapFrom(src => src.IdentityImagePath ?? string.Empty))
            .ForMember(dest => dest.Email,
                       opt => opt.MapFrom(src => src.Email ?? string.Empty))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastModifiedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}