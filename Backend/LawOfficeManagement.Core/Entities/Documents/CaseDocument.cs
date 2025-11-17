using LawOfficeManagement.Core.Entities.Cases;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Documents
{
    /// <summary>
    /// جدول المرفقات الخاصة بالقضية
    /// </summary>
    public class CaseDocument : BaseEntity
    {
        public int CaseId { get; set; }
        public virtual Case Case { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FileType { get; set; } // PDF, JPG, DOCX


       
    }
}
