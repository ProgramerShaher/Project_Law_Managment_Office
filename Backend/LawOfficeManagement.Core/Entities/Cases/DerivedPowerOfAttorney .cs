using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Document = LawOfficeManagement.Core.Entities.Documents.Document;

namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// //جدول الوكالة المشتقة من الوكالة الاساسية //الانارة
    /// </summary>
    public class DerivedPowerOfAttorney : BaseEntity
    {
        public int ParentPowerOfAttorneyId { get; set; }
        public PowerOfAttorney ParentPowerOfAttorney { get; set; }

        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        /// <summary>
        /// رقم الوكالة المشتقة (اختياري أو داخلي)
        /// </summary>
        [MaxLength(50)]
        public string DerivedNumber { get; set; } = string.Empty; 

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// الصلاحيات الممنوحة للمحامي
        /// </summary>
        [MaxLength(300)]
        public string? AuthorityScope { get; set; } 

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        /// <summary>
        ///  مسار لحفظ صورة لوكالة المشتقة
        /// </summary>
        public required string Derived_Document_Agent_Url { get; set; }

    }
}
