using MediatR;
using Microsoft.AspNetCore.Http;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.UpdateLawyer
{
    public class UpdateLawyerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public int LawyerType { get; set; }

        public string? IdentityImage { get; set; }
        public string? QualificationDocuments { get; set; }
    }
}
