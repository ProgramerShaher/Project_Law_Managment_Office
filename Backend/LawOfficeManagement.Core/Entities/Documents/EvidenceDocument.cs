using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Documents
{
    /// <summary>
    /// جدول مرفقات الأدلة
    /// </summary>
    public class EvidenceDocument : BaseEntity
    {
        [ForeignKey("CaseEvidence")]
        public int CaseEvidenceId { get; set; }
        public CaseEvidence CaseEvidence { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FileType { get; set; } // PDF, JPG, DOCX, إلخ

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
