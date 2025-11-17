namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs
{
    public class PowerOfAttorneyDto
    {
        public int Id { get; set; }
        public string AgencyNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuingAuthority { get; set; } = string.Empty;
        public string AgencyType { get; set; } = string.Empty;
        public bool DerivedPowerOfAttorney { get; set; }
        public string Document_Agent_Url { get; set; } = string.Empty;
        public string? ClientName { get; set; }
        public string? OfficeName { get; set; }
        public string? LawyerName { get; set; }
    }
}
