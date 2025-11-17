using LawOfficeManagement.Application.Features.Courts.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Courts.Commands.CreateCourt
{
    public class CreateCaseTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int CourtTypeId { get; set; }
        public string Address { get; set; }
        public List<CourtDivisionInputDto> Divisions { get; set; } = new();
    }
}
