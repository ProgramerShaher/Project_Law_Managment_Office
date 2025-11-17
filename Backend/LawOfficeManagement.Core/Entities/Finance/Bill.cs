using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    public class Bill :BaseEntity
    {
        /// <summary>
        /// رقم الفاتورة التسلسلي
        /// </summary>
        public string BillNumber { get; set; }

        public virtual IList<Bill_Item> _Items { get; set; }
        = new List<Bill_Item>();

        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public virtual Case Case { get; set; }
        // حالة الفاتورة: مسودة/مسلمة/مدفوعة/ملغية
        public BillStatus Status { get; set; }

        // تاريخ الاستحقاق
        public DateTime DueDate { get; set; }

        // رقم القيد المحاسبي المرتبط
        public int? JournalId { get; set; }
        public virtual Journal Journal { get; set; }
        public decimal Total => (decimal)_Items.Sum(e => e.TheAmount);
        /// <summary>
        /// الضريبة
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// الخصم
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// الصافي
        /// </summary>
        public decimal NetAmount { get; set; }
        /// <summary>
        /// رقم القيد المحاسبي المرتبط
        /// </summary>
      
        public enum BillStatus
        {
            Draft = 1,      // مسودة
            Issued = 2,     // مسلمة
            Paid = 3,       // مدفوعة
            Cancelled = 4   // ملغية
        }

    }
}
