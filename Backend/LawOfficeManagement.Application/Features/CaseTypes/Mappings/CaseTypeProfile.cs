using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTypes.Commands;
using LawOfficeManagement.Application.Features.CaseTypes.Queries.GetAllCaseTypes;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.MappingProfiles
{
    public class CaseTypeProfile : Profile
    {
        public CaseTypeProfile()
        {
            // CaseType mappings
            CreateMap<CreateCaseTypeCommand, CaseType>();
            CreateMap<CaseType, CaseTypeDto>();
          
            // CaseTypeCategory mappings
        }
    }
}