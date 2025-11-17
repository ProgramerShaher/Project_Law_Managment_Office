// Application/Common/Mappings/ContractMappingProfile.cs
using AutoMapper;
using LawOfficeManagement.Application.Features.Contracts.DTOs;
using LawOfficeManagement.Core.Entities.Contracts;

public class ContractMappingProfile : Profile
{
    public ContractMappingProfile()
    {
        CreateMap<CreateContractDto, Contract>();
        CreateMap<UpdateContractDto, Contract>();

        CreateMap<Contract, ContractDto>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
            .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case.Title))
            .ForMember(dest => dest.CalculatedAmount, opt => opt.MapFrom(src => src.CalculatedAmount));
    }
}