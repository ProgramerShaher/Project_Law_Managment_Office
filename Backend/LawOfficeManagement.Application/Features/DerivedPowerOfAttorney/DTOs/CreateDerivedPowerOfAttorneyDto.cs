using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.Application.Features.DerivedPowerOfAttorneys.DTOs
{
    public class CreateDerivedPowerOfAttorneyDto
    {
        [Required]
        public int ParentPowerOfAttorneyId { get; set; }

        [Required]
        public int LawyerId { get; set; }

        [MaxLength(50)]
        public string DerivedNumber { get; set; } = string.Empty;

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(300)]
        public string? AuthorityScope { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Required]
        public required string Derived_Document_Agent_Url { get; set; }
    }
}