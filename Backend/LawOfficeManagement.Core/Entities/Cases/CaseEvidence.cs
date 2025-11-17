using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// جدول الأدلة
    /// </summary>
    public class CaseEvidence : BaseEntity
    {
        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public Case Case { get; set; }
        /// <summary>
        /// الدليل قد يُقدم في جلسة محددة
        /// </summary>
        [ForeignKey("CaseSession")]
        public int? CaseSessionId { get; set; } 
        public virtual CaseSession? CaseSession { get; set; }

        /// <summary>
        /// اسم أو نوع الدليل (مثل: تقرير طبي)
        /// </summary>
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// مادي، رقمي، شهادة، وثيقة، إلخ
        /// </summary>
        [MaxLength(100)]
        public string? EvidenceType { get; set; } // مادي، رقمي، شهادة، وثيقة، إلخ
        /// <summary>
        /// وصف مختصر لمحتوى الدليل
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }
        /// <summary>
        /// مصدر الدليل (الشاهد، الشرطة، جهة رسمية...)
        /// </summary>
        [MaxLength(100)]
        public string? Source { get; set; }
        /// <summary>
        /// تاريخ تقديم الدليل
        /// </summary>
        public DateTime? SubmissionDate { get; set; }
        /// <summary>
        /// هل تم قبول الدليل من المحكمة
        /// </summary>
        public bool IsAccepted { get; set; } = false;
        /// <summary>
        /// ملاحظات القاضي أو المحكمة حول الدليل
        /// </summary>
        [MaxLength(500)]
        public string? CourtNotes { get; set; } 

    }
}
