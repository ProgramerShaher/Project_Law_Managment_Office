using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LawOfficeManagement.Core.Entities.Cases
{
    public class CaseType : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [JsonIgnore] 
        public virtual ICollection<Case>? Cases { get; set; } 
    }
}