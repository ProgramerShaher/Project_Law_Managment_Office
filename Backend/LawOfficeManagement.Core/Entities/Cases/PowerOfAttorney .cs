using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawOfficeManagement.Core.Entities.Documents;
using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Core.Entities. Cases
{
    /// <summary>
    ///جدول الوكالات 
    /// </summary>
    public class PowerOfAttorney : BaseEntity
    {
        // 🔹 معلومات أساسية
        /// <summary>
        /// رقم الوكالة الرسمي من الجهة المصدرة
        /// </summary>
        [Required, MaxLength(50)]
        public string? AgencyNumber { get; set; } = string.Empty;
        /// <summary>
        /// تاريخ إصدار الوكالة
        /// </summary>
        [Required]
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// تاريخ انتهاء الوكالة (إن وجِد)
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// الجهة المصدرة (مثل كاتب العدل، وزارة العدل...)
        /// </summary>
        [Required, MaxLength(200)]
        public string IssuingAuthority { get; set; } = string.Empty;

        // 👤 أطراف الوكالة

        /// <summary>
        /// صاحب الوكالة (العميل
        /// او اكثر ) 
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client? Client  { get; set; }


        /// <summary>
        /// المكتب الموكَّل
        /// </summary>
        public int? OfficeID { get; set; }                           
        public virtual Office ? Office { get; set; }

        public int? LawyerID { get; set; }
        public virtual Lawyer? Lawyer { get; set; }

            

        /// <summary>
        ///نوع الوكالة (عامة، خاصة، جزئية...)
        /// </summary>
        [Required, MaxLength(100)]
        public string AgencyType { get; set; } = string.Empty;

        /// <summary>
        /// امكانية التوكيل من الوكالة
        /// </summary>
        public bool DerivedPowerOfAttorney { get; set; } 


        /// <summary>
        /// 🧾 حالة الوكالة
        /// </summary>   
        public virtual AgencyStatus Status { get; set; } = AgencyStatus.Active;

        /// <summary>
        ///  مسار لحفظ صورة لوكالة 
        /// </summary>
        public required string Document_Agent_Url { get; set; } 
        public virtual ICollection<DerivedPowerOfAttorney>? DerivedPowerOfAttorneys { get; set; } =
           new List<DerivedPowerOfAttorney>();

        //public virtual ICollection<Case>? Cases { get; set; } 
               
    }
    public enum AgencyStatus
    {
        /// <summary>
        /// سارية /نشطة
        /// </summary>
        Active,
        /// <summary>
        /// منتهية
        /// </summary>
        Expand,
        

    }

}
