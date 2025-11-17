using LawOfficeManagement.Core.Entities.Cases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Documents
{
    public class TaskDocument : BaseEntity
    {
        [Required]
        public int TaskItemId { get; set; }
        public virtual TaskItem TaskItem { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FileType { get; set; }

    }

}
