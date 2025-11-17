using System.ComponentModel.DataAnnotations;
using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Core.Entities.Contracts
{
    /// <summary>
    /// عقود العملاء مع المكتب
    /// </summary>
    public class Contract: BaseEntity
    {
        // 🔹 معلومات أساسية

        /// <summary>
        ///  رقم العقد الداخلي أو الرسمي
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "رقم العقد")]
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// عنوان العقد 
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان العقد")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Display(Name = "نص العقد")]
        /// <summary>
        ///  نص العقد 
        /// </summary>
        public string ContractDescription { get; set; }


        /// <summary>
        /// تاريخ بدء العقد
        /// </summary>
        [Required]
        [Display(Name = "تاريخ البدء")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// تاريخ انتهاء العقد (إن وجد)
        /// </summary>
        [Display(Name = "تاريخ الانتهاء")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///  العلاقة مع العميل
        /// </summary>
        [Display(Name = "العميل")]
        public int ClientId { get; set; }

        [Display(Name = "العميل")]
        public virtual Client Client { get; set; } = null!;

        /// <summary>
        ///  العلاقة مع القضية (واحد إلى واحد)
        /// </summary>
        [Display(Name = "القضية")]
        public int CaseId { get; set; }

        [Display(Name = "القضية")]
        public virtual Case Case { get; set; } = null!;

        /// <summary>
        /// حالة العقد
        /// </summary>
        [Display(Name = "حالة العقد")]
        public ContractStatus Status { get; set; } = ContractStatus.Active;

        /// <summary>
        /// نوع الاتفاق المالي
        /// </summary>
        [Display(Name = "نوع الاتفاق المالي")]
        public FinancialAgreementType FinancialAgreementType { get; set; }

        /// <summary>
        /// مبلغ القضية الكلي
        /// </summary> 
        [Display(Name = "مبلغ القضية الكلي")]
        public decimal? TotalCaseAmount { get; set; }

        /// <summary>
        /// النسبة المئوية من مبلغ القضية
        /// </summary>
        [Display(Name = "النسبة المئوية")]
        [Range(0, 100)]
        public int? Percentage { get; set; }

        /// <summary>
        /// المبلغ النهائي المتفق عليه
        /// </summary>
        [Display(Name = "المبلغ النهائي")]
        public decimal? FinalAgreedAmount { get; set; }

        /// <summary>
        /// رابط مستند العقد
        /// </summary>
        [Display(Name = "مستند العقد")]
        [MaxLength(500)]
        public string? ContractDocumentUrl { get; set; }

        /// <summary>
        /// حساب المبلغ المستحق بناءً على نوع الاتفاق
        /// </summary>
        [Display(Name = "المبلغ المستحق")]
        public decimal? CalculatedAmount
        {
            get
            {
                return FinancialAgreementType switch
                {
                    FinancialAgreementType.PercentageBased when TotalCaseAmount.HasValue && Percentage.HasValue
                        => TotalCaseAmount.Value * (Percentage.Value / 100m),
                    FinancialAgreementType.FixedAmount => FinalAgreedAmount,
                    FinancialAgreementType.ServiceFees => FinalAgreedAmount, // أو منطق مختلف لرسوم الخدمات
                    _ => null
                };
            }
        }
    }

    /// <summary>
    /// نوع الاتفاق المالي
    /// </summary>
    public enum FinancialAgreementType
    {
        /// <summary>
        /// نسبة من مبلغ القضية
        /// </summary>
        [Display(Name = "نسبة من مبلغ القضية")]
        PercentageBased = 1,

        /// <summary>
        /// مبلغ ثابت متفق عليه من البداية إلى النهاية
        /// </summary>
        [Display(Name = "مبلغ ثابت")]
        FixedAmount = 2,

        /// <summary>
        /// مبلغ على كل خدمة يتم تقديمها للعميل (أتعاب)
        /// </summary>
        [Display(Name = "أتعاب حسب الخدمة")]
        ServiceFees = 3
    }

    /// <summary>
    /// حالة العقد
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// ساري المفعول
        /// </summary>
        [Display(Name = "ساري المفعول")]
        Active = 0,

        /// <summary>
        /// مكتمل
        /// </summary>
        [Display(Name = "مكتمل")]
        Completed = 1,

        /// <summary>
        /// ملغي
        /// </summary>
        [Display(Name = "ملغي")]
        Cancelled = 2,

        /// <summary>
        /// موقوف مؤقتًا
        /// </summary>
        [Display(Name = "موقوف مؤقتاً")]
        Suspended = 3,

        /// <summary>
        /// منتهي الصلاحية
        /// </summary>
        [Display(Name = "منتهي الصلاحية")]
        Expired = 4,

        /// <summary>
        /// قيد المراجعة
        /// </summary>
        [Display(Name = "قيد المراجعة")]
        UnderReview = 5,

        /// <summary>
        /// مرفوض
        /// </summary>
        [Display(Name = "مرفوض")]
        Rejected = 6
    }
}