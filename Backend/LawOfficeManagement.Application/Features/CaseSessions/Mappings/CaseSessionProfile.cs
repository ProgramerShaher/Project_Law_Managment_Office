using AutoMapper;
using LawOfficeManagement.Application.Features.Cases.Commands.Dtos;
using LawOfficeManagement.Application.Features.Cases.Queries.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Features.CaseSessions.Mappings
{
    public class CaseSessionProfile : Profile
    {
        public CaseSessionProfile()
        {
            // =============================================
            // 🔹 CREATE MAPPINGS
            // =============================================

            CreateMap<CreateCaseSessionDto, CaseSession>()
                .ForMember(dest => dest.SessionStatus,
                    opt => opt.MapFrom(_ => Core.Enums.CaseSessionStatus.Pending))
                .ForMember(dest => dest.LawyerAttended,
                    opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ClientAttended,
                    opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.CaseEvidences,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CaseWitnesses,
                    opt => opt.Ignore());

            CreateMap<CreateCaseEvidenceDto, CaseEvidence>();
            CreateMap<CreateCaseWitnessDto, CaseWitness>();

            // =============================================
            // 🔹 UPDATE MAPPINGS - الاصلاح هنا
            // =============================================

            CreateMap<UpdateCaseSessionDto, CaseSession>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CaseId, opt => opt.Ignore())
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.Court, opt => opt.Ignore())
                .ForMember(dest => dest.CourtDivision, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedLawyer, opt => opt.Ignore())
                .ForMember(dest => dest.CaseEvidences, opt => opt.Ignore())
                .ForMember(dest => dest.CaseWitnesses, opt => opt.Ignore())
                .ForMember(dest => dest.Documents, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // 🔹 الاصلاح الرئيسي: UpdateCaseEvidenceDto -> CaseEvidence
            CreateMap<UpdateCaseEvidenceDto, CaseEvidence>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // تجاهل الـ Id لأنه موجود مسبقاً
                .ForMember(dest => dest.CaseId, opt => opt.Ignore())
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.CaseSession, opt => opt.Ignore())
                .ForMember(dest => dest.CaseSessionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // 🔹 الاصلاح الرئيسي: UpdateCaseWitnessDto -> CaseWitness
            CreateMap<UpdateCaseWitnessDto, CaseWitness>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // تجاهل الـ Id لأنه موجود مسبقاً
                .ForMember(dest => dest.CaseId, opt => opt.Ignore())
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.CaseSession, opt => opt.Ignore())
                .ForMember(dest => dest.CaseSessionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =============================================
            // 🔹 QUERY MAPPINGS
            // =============================================

            CreateMap<CaseSession, CaseSessionDto>()
                .ForMember(dest => dest.CaseNumber,
                    opt => opt.MapFrom(src => src.Case != null ? src.Case.CaseNumber : string.Empty))
                .ForMember(dest => dest.CaseTitle,
                    opt => opt.MapFrom(src => src.Case != null ? src.Case.Title : string.Empty))
                .ForMember(dest => dest.CourtName,
                    opt => opt.MapFrom(src => src.Court.Name))
                .ForMember(dest => dest.CourtDivisionName,
                    opt => opt.MapFrom(src => src.CourtDivision.Name))
                .ForMember(dest => dest.AssignedLawyerName,
                    opt => opt.MapFrom(src => src.AssignedLawyer != null ?
                        src.AssignedLawyer.FullName : string.Empty));

            CreateMap<CaseEvidence, CaseEvidenceDto>();
            CreateMap<CaseWitness, CaseWitnessDto>();
        }
    }
}