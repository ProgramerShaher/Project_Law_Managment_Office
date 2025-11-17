namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs
{
    public class DerivedPowerOfAttorneyDto
    {
        public int Id { get; set; }
        public int ParentPowerOfAttorneyId { get; set; }
        public string ParentPowerOfAttorneyNumber { get; set; } = string.Empty;
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public string DerivedNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? AuthorityScope { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public string Derived_Document_Agent_Url { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }
}