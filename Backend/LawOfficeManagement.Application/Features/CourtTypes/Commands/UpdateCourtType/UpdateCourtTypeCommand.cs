using MediatR;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.UpdateCourtType
{
    public class UpdateCourtTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
