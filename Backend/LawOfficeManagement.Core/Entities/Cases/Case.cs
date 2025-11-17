
using LawOfficeManagement.Core.Entities.Contracts;
using LawOfficeManagement.Core.Entities.Documents;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Contract = LawOfficeManagement.Core.Entities.Contracts.Contract;
namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// جدول القضايا
    /// </summary>
    public class Case : BaseEntity
    {
        // 🏷️ معلومات التعريف الأساسية

        /// <summary>
        /// عنوان القضية
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// وصف عام للقضية
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// رقم القضية الرسمي بالمحكمة
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CaseNumber { get; set; } = string.Empty;

        /// <summary>
        /// رقم القضية في النيابة
        /// </summary>
        [MaxLength(50)]
        public string? CaseNumberProsecution { get; set; }

        /// <summary>
        /// رقم القضية في الشرطة
        /// </summary>
        [MaxLength(50)]
        public string? CaseNumberInPolice { get; set; }

        /// <summary>
        /// رقم مرجعي داخلي داخل المكتب
        /// </summary>
        [MaxLength(100)]
        public string? InternalReference { get; set; }

        // 🗓️ المعلومات الزمنية

        /// <summary>
        /// تاريخ رفع الدعوى
        /// </summary>
        [Required]
        public DateTime FilingDate { get; set; }

        /// <summary>
        /// تاريخ أول جلسة في القضية
        /// </summary>
        public DateTime? FirstSessionDate { get; set; }



        // ⚖️ معلومات المحكمة

        /// <summary>
        /// نوع المحكمة (ابتدائية، استئناف، نقض، إلخ)
        /// </summary>
        public int? CourtTypeId { get; set; }
        public virtual CourtType? CourtType { get; set; }




        /// <summary>
        /// المحكمة المرفوعة فيها القضية
        /// </summary>
        public int? CourtId { get; set; }
        public virtual Court? Court { get; set; } = null!;

        /// <summary>
        /// القسم أو الدائرة القضائية في المحكمة
        /// </summary>

        public int? CourtDivisionId { get; set; }
        public virtual CourtDivision? CourtDivision { get; set; }


        // 👥 الأطراف

        /// <summary>
        /// العميل صاحب القضية
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;     



        /// <summary>
        ///مراحل القضية
        /// </summary>            
        public virtual ICollection<CaseStage>? CaseStages { get; set; } =
            new List<CaseStage>();
        /// <summary>
        /// جلسات القضية
        /// </summary>
        public virtual ICollection<CaseSession>? CaseSessions { get; set; } =
            new List<CaseSession>();
        /// <summary>
        ///  الادلة
        /// </summary>
        public virtual ICollection<CaseEvidence>? CaseEvidences { get; set; } =
            new List<CaseEvidence>();
        /// <summary>
        /// الشهود
        /// </summary>
        public virtual ICollection<CaseWitness>? CaseWitnesses { get; set; } =
            new List<CaseWitness>();

        /// <summary>
        /// الوكالة  
        /// </summary>
        public int? PowerOfAttorneyId { get; set; }
        public virtual PowerOfAttorney? PowerOfAttorney{ get; set; }
//>>>>>>> 850fa72f03ddf0fb44a278d49c3b080a45a77601

        /// <summary>
        /// العقد المرتبط بالقضية (واحد إلى واحد)
        /// </summary>
        [Display(Name = "العقد")]
        public virtual Contract? Contract { get; set; }

        //public int? PowerOfAttorneyId { get; set; }
        //public virtual PowerOfAttorney? PowerOfAttorney { get; set; }

        public string ?PrincipalMandator
        {
            get
            {
                if (PowerOfAttorney == null)
                    return string.Empty;

                if (PowerOfAttorney.Office != null)
                    return PowerOfAttorney.Office.OfficeName ?? string.Empty;

                if (PowerOfAttorney.Lawyer != null)
                    return $"{PowerOfAttorney.Lawyer.FullName}";

                return string.Empty;
            }
        }

        /// <summary>
        ///فريق عمل القضية
        /// </summary>            
        public virtual ICollection<CaseTeam>? CaseTeams { get; set; } =
            new List<CaseTeam>();


        /// <summary>
        /// الخصم في القضية
        /// </summary>
        public int? OpponentId { get; set; }
        public virtual Opponent? Opponents { get; set; }

        // 📋 حالة ونوع القضية

        /// <summary>
        /// حالة القضية (جارية، منتهية، مؤجلة، إلخ)
        /// </summary>
        [Required]
        public CaseStatus Status { get; set; }

        /// <summary>
        /// معرف نوع القضية
        /// </summary>
        public int?  CaseTypeId { get; set; }
        public virtual CaseType CaseType { get; set; }


        /// <summary>
        /// المستندات والملفات المرفقة بالقضية
        /// </summary>
        public virtual ICollection<CaseDocument>? CaseDocuments { get; set; } = new List<CaseDocument>();


        /// <summary>
        /// ملاحظات إدارية أو قانونية حول القضية
        /// </summary>
        [MaxLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// يشير إلى ما إذا كانت القضية مؤرشفة
        /// </summary>
        public bool IsArchived { get; set; } = false;

        /// <summary>
        /// يشير إلى ما إذا كانت القضية سرية
        /// </summary>
        public bool IsConfidential { get; set; } = false;

        /// <summary>
        /// نتيجة القضية (فاز، خسر، صلح، إلخ)
        /// </summary>
        [MaxLength(200)]
        public string? Outcome { get; set; }


    }

    /// <summary>
    /// حالة القضية
    /// </summary>
    public enum CaseStatus
    {
        /// <summary>
        /// قيد الإعداد
        /// </summary>
        Draft = 0,

        /// <summary>
        /// جارية
        /// </summary>
        Ongoing = 1,

        /// <summary>
        /// مؤجلة
        /// </summary>
        Postponed = 2,

        /// <summary>
        /// منتهية
        /// </summary>
        Closed = 3,

        /// <summary>
        /// محكوم فيها
        /// </summary>
        Judged = 4,

        /// <summary>
        /// مستأنفة
        /// </summary>
        Appealed = 5,

        /// <summary>
        /// ملغاة
        /// </summary>
        Cancelled = 6
    }
}