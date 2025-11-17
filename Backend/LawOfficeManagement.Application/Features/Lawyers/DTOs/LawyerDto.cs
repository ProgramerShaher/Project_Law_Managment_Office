using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.Lawyers.DTOs
{
    public class LawyerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentityImagePath { get; set; }
        public string QualificationDocumentsPath { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public LawyerType Type { get; set; }
    }
}
