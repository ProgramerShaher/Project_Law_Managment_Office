using MediatR;
using Microsoft.AspNetCore.Http;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.CreatePowerOfAttorney
{
    public class CreatePowerOfAttorneyCommand : IRequest<int>
    {
        public string AgencyNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuingAuthority { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int? OfficeID { get; set; }
        public int? LawyerID { get; set; }
        public string AgencyType { get; set; } = string.Empty;
        public bool DerivedPowerOfAttorney { get; set; } = true;
        public string Document_Agent_Url { get; set; } = string.Empty;

        public string? DocumentFile { get; set; } // صورة الوكالة
    }
}
