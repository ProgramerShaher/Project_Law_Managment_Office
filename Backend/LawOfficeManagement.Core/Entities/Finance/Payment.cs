using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Finance
{
    public class Payment : BaseEntity
    {
        [ForeignKey("Bill")]
        public  int Bill_Id { get; set; }
        public virtual Bill Bill { get; set; }
        /// <summary>
        /// المبلغ المستحق
        /// </summary>
        [Required]
        public decimal The_amount_payable { get; set; }
        /// <summary>
        /// المبلغ المدفوع
        /// </summary>
        public decimal The_amount_paid { get; set; }
        /// <summary>
        /// المبلغ المتبقي
        /// </summary>
        public decimal The_amount_remaining => The_amount_payable - The_amount_paid;

        [MaxLength(200)]
        public string? PaymentMethod { get; set; } // نقد، تحويل، شيك...

        [MaxLength(500)]
        public string? Notes { get; set; } // ملاحظات إضافية
    }
}
