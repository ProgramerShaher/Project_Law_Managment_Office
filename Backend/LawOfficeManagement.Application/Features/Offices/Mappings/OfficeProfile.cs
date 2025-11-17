using AutoMapper;
using LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer;
using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using LawOfficeManagement.Application.Features.Offices.Commands.Add;
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.Lawyers.Mappings
{
    /// <summary>
    /// يحتوي على جميع عمليات التحويل (Mapping) الخاصة بكيان المحامي.
    /// </summary>
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, AddOfficeCommand>().ReverseMap();
        }
    }
}
