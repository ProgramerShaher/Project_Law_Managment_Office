using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.CreateCourtType
{
    public class CreateCourtTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
