using MediatR;
using Microsoft.AspNetCore.Http;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.UpdatePowerOfAttorney
{
    public class UpdatePowerOfAttorneyCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string AgencyNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuingAuthority { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int? OfficeID { get; set; }
        public int? LawyerID { get; set; }
        public string AgencyType { get; set; } = string.Empty;
        public bool DerivedPowerOfAttorney { get; set; }
        public IFormFile? NewDocumentFile { get; set; }
    }
}
