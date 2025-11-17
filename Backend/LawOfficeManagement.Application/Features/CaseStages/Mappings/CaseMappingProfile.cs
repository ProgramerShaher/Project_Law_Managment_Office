using AutoMapper;
using LawOfficeManagement.Application.Features.Cases.Commands.UpdateCaseStage;
using LawOfficeManagement.Application.Features.CaseStages.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.CaseStages.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {

            CreateMap<CreateCaseStageDto, CaseStage>();
            CreateMap<UpdateCaseStageDto, CaseStage>();
            CreateMap<CaseStage, CaseStageDetailsDto>()
                .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case!.Title))
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case!.CaseNumber));
            CreateMap<CaseStage, CaseStageListDto>()
                .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case!.Title));
        }
    }
}
