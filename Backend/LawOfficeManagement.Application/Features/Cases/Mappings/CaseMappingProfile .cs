using AutoMapper;
using LawOfficeManagement.Application.Features.Cases.Dtos;
using LawOfficeManagement.Application.Features.Cases.Queries.GetAllCases;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Mappings
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            // Create
            CreateMap<CreateCaseDto, Case>();

            // Update
            CreateMap<UpdateCaseDto, Case>();

            // Details
            CreateMap<Case, CaseDetailsDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court.Name))
                .ForMember(dest => dest.CourtTypeName, opt => opt.MapFrom(src => src.CourtType!.Name))
                .ForMember(dest => dest.CourtDivisionName, opt => opt.MapFrom(src => src.CourtDivision!.Name))
                .ForMember(dest => dest.OpponentName, opt => opt.MapFrom(src => src.Opponents!.OpponentName))
                .ForMember(dest => dest.CaseTypeName, opt => opt.MapFrom(src => src.CaseType.Name))
                .ForMember(dest => dest.PrincipalMandator, opt => opt.MapFrom(src => src.PrincipalMandator));

            // List (يستخدم في GetAllCases و GetCasesByClient)
            CreateMap<Case, CaseListDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court.Name))
                .ForMember(dest => dest.CaseTypeName, opt => opt.MapFrom(src => src.CaseType.Name));
        }
    }
}