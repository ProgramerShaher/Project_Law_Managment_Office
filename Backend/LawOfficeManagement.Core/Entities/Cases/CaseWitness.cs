using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Documents;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// جدول الشهود
    /// </summary>
    public class CaseWitness : BaseEntity
    {
        /// <summary>
        /// ربط الشاهد بالقضية
        /// </summary>
        [ForeignKey("Case")]
        public int CaseId { get; set; }       
        public Case Case { get; set; }
        /// <summary>
        /// إن كان الشاهد ظهر في جلسة معينة
        /// </summary>
        [ForeignKey("CaseSession")]
        public int? CaseSessionId { get; set; }  
        public CaseSession? CaseSession { get; set; }
        /// <summary>
        /// اسم الشاهد الكامل
        /// </summary>
        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty; 
        /// <summary>
        /// رقم الهوية / البطاقة
        /// </summary>
        [MaxLength(50)]
        public string? NationalId { get; set; }  
        /// <summary>
        /// رقم الهاتف للتواصل
        /// </summary>
        [MaxLength(50)]
        public string? PhoneNumber { get; set; } 
        /// <summary>
        /// العنوان أو جهة السكن
        /// </summary>
        [MaxLength(200)]
        public string? Address { get; set; }     
        /// <summary>
        /// ملخص الشهادة
        /// </summary>
        [MaxLength(2000)]
        public string? TestimonySummary { get; set; }

        /// <summary>
        /// هل حضر الجلسة فعلاً
        /// </summary>
        public bool IsAttended { get; set; } = false; 
        /// <summary>
        /// تاريخ الإدلاء بالشهادة
        /// </summary>
        public DateTime? TestimonyDate { get; set; }
        /// <summary>
        /// ملاحظات إضافية عن الشاهد
        /// </summary>
        [MaxLength(500)]
        public string? Notes { get; set; }

      

    }
}
