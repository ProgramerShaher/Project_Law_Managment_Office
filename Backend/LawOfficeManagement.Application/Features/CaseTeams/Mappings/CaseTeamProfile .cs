using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTeams.Commands.CreateCaseTeam;
using LawOfficeManagement.Application.Features.CaseTeams.Commands.UpdateCaseTeam;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamById;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByCaseId;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamByLawyerId;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetCaseTeamMembersWithDetails;
using LawOfficeManagement.Application.Features.CaseTeams.Queries.GetLawyersAvailableForCase;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Application.Features.CaseTeams.DTOs;

namespace LawOfficeManagement.Application.Mappings
{
    public class CaseTeamProfile : Profile
    {
        public CaseTeamProfile()
        {
            // Create
            CreateMap<CreateCaseTeamDto, CaseTeam>();

            // Update
            CreateMap<UpdateCaseTeamDto, CaseTeam>();

            // Details
            CreateMap<CaseTeam, CaseTeamDetailsDto>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.FullName))
                .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case.Title))
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case.CaseNumber));
                //.ForMember(dest => dest.PowerOfAttorneyNumber,
                //opt => opt.MapFrom(src => src.DerivedPowerOfAttorney != null ? src.DerivedPowerOfAttorney.DerivedNumber : null))


            // List
            CreateMap<CaseTeam, CaseTeamListDto>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.FullName));
              

            // Lawyer Cases
            CreateMap<CaseTeam, CaseTeamLawyerDto>()
                .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case.Title))
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case.CaseNumber))
                .ForMember(dest => dest.CaseStatus, opt => opt.MapFrom(src => src.Case.Status.ToString()));

            // Member Details
            CreateMap<CaseTeam, CaseTeamMemberDetailsDto>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.FullName))
                .ForMember(dest => dest.LawyerEmail, opt => opt.MapFrom(src => src.Lawyer.Email))
                .ForMember(dest => dest.LawyerPhone, opt => opt.MapFrom(src => src.Lawyer.PhoneNumber));
            //.ForMember(dest => dest.PowerOfAttorneyNumber,
            //    opt => opt.MapFrom(src => src.DerivedPowerOfAttorney != null ? src.DerivedPowerOfAttorney.DerivedNumber : null))
            //.ForMember(dest => dest.PowerOfAttorneyDate,
            //    opt => opt.MapFrom(src => src.DerivedPowerOfAttorney != null ? src.DerivedPowerOfAttorney.CreatedOn : (DateTime?)null))
            //.ForMember(dest => dest.HasDerivedPowerOfAttorney,
            //opt => opt.MapFrom(src => src.DerivedPowerOfAttorneyId.HasValue));

            // Available Lawyers
            CreateMap<Lawyer, AvailableLawyerDto>()
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
                //.ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization));
        }
    }
}