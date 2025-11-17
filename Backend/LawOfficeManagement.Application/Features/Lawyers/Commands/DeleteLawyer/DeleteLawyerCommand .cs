using MediatR;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.DeleteLawyer
{
    public class DeleteLawyerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
