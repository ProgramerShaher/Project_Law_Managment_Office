using LawOfficeManagement.Core.Entities.Cases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// يمثل سجل استشارة قانونية سواء كانت مكتبية أو هاتفية .
    /// </summary>
    public class LegalConsultation : BaseEntity
    {
        /// <summary>
        /// اسم طالب الاستشارة  
        /// </summary>
        public string? CustomerName { get; set; } 
        public string  MobileNumber { get; set; }
        public string? MobileNumber2 { get; set; }
        public string ?Email { get; set; }

        // 🔹 المحامي أو المستشار الذي قدّم الاستشارة
        [ForeignKey("Lawyer")]
        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

        // 🔹 تفاصيل الاستشارة
        /// <summary>
        /// عنوان أو موضوع الاستشارة 
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        /// <summary>
        /// محتوى الاستشارة بالتفصيل
        /// </summary>
        [Required]
        public string? Details { get; set; } = string.Empty; 

        /// <summary>
        /// 🔹 نوع الاستشارة (مكتبية، هاتفية، عبر البريد، جلسة...)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ConsultationType { get; set; } = "مكتبية";
        /// <summary>
        /// الخدمة المقدمة مع السعر
        /// </summary>
        public int ServiceOfficeId { get; set; }
        public virtual ServiceOffice ServiceOffice { get; set; } 
     
        [MaxLength(500)]
        public string? Notes { get; set; }
        /// <summary>
        /// مسار لصورة الاستشارة اختياري
        /// </summary>
        public string? UrlLegalConsultation { get; set; }
        /// <summary>
        /// مسار الفاتورة
        /// </summary>
        public string? UrlLegalConsultationInvoice { get; set; }

        // 🔹 حالة الاستشارة
        /// <summary>
        /// أمثلة: قيد التنفيذ – مكتملة – مؤجلة – ملغاة
        /// </summary>
        [MaxLength(50)]
        public string ? Status { get; set; } = "مكتملة";
        

     

    }
}
