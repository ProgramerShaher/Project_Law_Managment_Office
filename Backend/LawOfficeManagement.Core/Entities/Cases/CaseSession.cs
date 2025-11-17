using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawOfficeManagement.Core.Entities.Documents;
using LawOfficeManagement.Core.Entities.Finance;
using LawOfficeManagement.Core.Enums;


namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// جدول الجلسات 
    /// </summary>
    public class CaseSession : BaseEntity
    {
        /// <summary>
        /// ربط الجلسة بالقضية
        /// </summary>
        [ForeignKey("Case")]
        public int? CaseId { get; set; } 
        public Case? Case { get; set; }

        /// <summary>
        /// المحكمة التي تُعقد فيها الجلسة
        /// </summary>
        [ForeignKey("Court")]
        public int CourtId { get; set; } 
        public Court Court { get; set; }

        /// <summary>
        ///دائرة المحكمة التي تُعقد فيها الجلسة 
        /// </summary>
        [ForeignKey("CourtDivision")]
        public int CourtDivisionId { get; set; }
        public CourtDivision CourtDivision { get; set; }
        /// <summary>
        /// المحامي المكلف بحضور الجلسة
        /// </summary>
        [ForeignKey("AssignedLawyer")]
        public int? AssignedLawyerId { get; set; } 
        public Lawyer? AssignedLawyer { get; set; }
        /// <summary>
        /// تاريخ الجلسة
        /// </summary>
        [Required]
        public DateTime SessionDate { get; set; }
        /// <summary>
        /// وقت الجلسة (نص حر مثل "10:00 صباحًا")
        /// </summary>
        [MaxLength(100)]
        public string? SessionTime { get; set; }
        /// <summary>
        /// رقم الجلسة الرسمي إن وجد
        /// </summary>
        [MaxLength(100)]
        public string? SessionNumber { get; set; }
        /// <summary>
        /// نوع الجلسة: "تمهيدية"، "مرافعة"، "نطق بالحكم"، إلخ
        /// </summary>
        [MaxLength(50)]
        public string? SessionType { get; set; }
        ///// <summary>
        ///// الحالة: "منعقدة"، "مؤجلة"، "محجوزة للحكم"، "منتهية"
        ///// </summary>
        //[MaxLength(50)]
        //public string? SessionStatus { get; set; }


        /// <summary>
        /// مكان الجلسة (في المحكمة أو عبر منصة إلكترونية)
        /// </summary>
        [MaxLength(500)]
        public string? Location { get; set; }
        /// <summary>
        /// ملاحظات عامة
        /// </summary>
        [MaxLength(1000)]
        public string? Notes { get; set; } 

        /// <summary>
        ///  القرار / الحكم / الإجراء الصادر
        /// </summary>
        [MaxLength(2000)]
        public string? Decision { get; set; }

        /// <summary>
        /// 🔹 التاريخ القادم للجلسة التالية (إن وُجد)
        /// </summary>
        public DateTime? NextSessionDate { get; set; } 


        /// <summary>
        /// 🔹 هل تم حضور الجلسة من قبل المحامي؟
        /// </summary>
        public bool LawyerAttended { get; set; } = false;

        /// <summary>
        /// 🔹 هل تم حضور الجلسة من قبل العميل؟
        /// </summary>
        public bool ClientAttended { get; set; } = false;

        ///<summary>
        /// الحالة: "قيد التنفيذ"، "مؤجلة"، "محجوزة للحكم"، "منتهية"
        /// </summary>
        public CaseSessionStatus SessionStatus { get; set; } = CaseSessionStatus.Pending;

        /// <summary>
        /// 🔹 مرفقات الجلسة (مثل محضر الجلسة أو المستندات )
        /// </summary>
        public virtual ICollection<SessionDocument>? Documents { get; set; } 
            = new List<SessionDocument>();
        /// <summary>
        /// الادلة المقدمة
        /// </summary>
        public virtual ICollection<CaseEvidence> ?CaseEvidences { get; set; } 
            = new List<CaseEvidence>();
        /// <summary>
        /// الشهود 
        /// </summary>
        public virtual  ICollection<CaseWitness> ?CaseWitnesses { get; set; } 
            = new List<CaseWitness>();

    }
}
