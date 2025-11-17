using LawOfficeManagement.Core.Entities.Cases;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Documents
{
    /// <summary>
    /// جدول المرفقات الخاصة بالجلسة
    /// </summary>
    public class SessionDocument : BaseEntity
    {
        [ForeignKey("CaseSession")]
        public int CaseSessionId { get; set; }
        public CaseSession CaseSession { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FileType { get; set; } // PDF, JPG, DOCX

        public DateTime UploadedAt { get; set; } = DateTime.Now;

       
    }
}
