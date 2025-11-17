using AutoMapper;
using LawOfficeManagement.Application.Features.CourtTypes.DTOs;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Application.Features.CourtTypes.Mappings
{
    public class CourtTypeProfile : Profile
    {
        public CourtTypeProfile()
        {
            CreateMap<CourtType, CourtTypeDto>();
        }
    }
}
