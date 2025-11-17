// LawOfficeManagement.Application.Features.Opponents.Mappings
using AutoMapper;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.Opponents.Mappings
{
    public class OpponentMappingProfile : Profile
    {
        public OpponentMappingProfile()
        {
            // Create
            CreateMap<CreateOpponentDto, Opponent>();

            // Update
            CreateMap<UpdateOpponentDto, Opponent>();
                
            // Entity to DTO
            CreateMap<Opponent, OpponentDto>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => GetTypeName(src.Type)))
                .ForMember(dest => dest.CasesCount, opt => opt.MapFrom(src => src.cases.Count));

            // For OpponentCaseDto
            CreateMap<Opponent, OpponentCaseDto>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => GetTypeName(src.Type)))
                .ForMember(dest => dest.Cases, static opt => opt.MapFrom(src => src.cases.Select(c => new OpponentCaseInfoDto
                {
                    CaseId = c.Id,
                    CaseNumber = c.CaseNumber,
                    CaseType = c.CaseType.ToString(),
                    Status = c.Status.ToString()
                })));
        }

        private static string GetTypeName(OpponentType type)
        {
            return type switch
            {
                OpponentType.Individual => "فرد",
                OpponentType.Company => "شركة",
                OpponentType.Person => "شخص",
                _ => "غير محدد"
            };
        }
    }
}