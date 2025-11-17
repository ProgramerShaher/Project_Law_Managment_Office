using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    /// <summary>
    /// جدول نقاط القيد
    /// </summary>
    public class JournalEntry
    {
        // رقم نقطة القيد (مفتاح أساسي)
        public int EntryId { get; set; }

        /// <summary>
        /// رقم القيد (مفتاح أجنبي)
        /// </summary>
        public int JournalId { get; set; }
        public virtual Journal Journal { get; set; }

        /// <summary>
        /// رقم الحساب (مفتاح أجنبي)
        /// </summary>
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        /// <summary>
        /// قيمة المدين
        /// </summary>
        public decimal DebitAmount { get; set; }

        /// <summary>
        /// قيمة الدائن
        /// </summary>
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// وصف نقطة القيد
        /// </summary>
        public string Description { get; set; }

        // رقم المستند المرجعي (إن وجد)
        public string ReferenceNumber { get; set; }

        // نوع المستند المرجعي (فاتورة، شيك، إلخ)
        public string ReferenceType { get; set; }

        // تاريخ القيد التفصيلي
        public DateTime EntryDate { get; set; }
    }
}
