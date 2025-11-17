using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    /// <summary>
    /// جدول اليومية
    /// </summary>
    public class Journal
    {
        // رقم القيد (مفتاح أساسي)
        public int JournalId { get; set; }

        // رقم القيد التسلسلي
        public string JournalNumber { get; set; }

        // تاريخ القيد
        public DateTime JournalDate { get; set; }

        // وصف القيد
        public string Description { get; set; }

        // إجمالي مدين القيد
        public decimal TotalDebit { get; set; }

        // إجمالي دائن القيد
        public decimal TotalCredit { get; set; }

        // حالة القيد: مسجل/معتمد/ملغى
        public JournalStatus Status { get; set; }

        // نوع القيد: قيد عادي/قيد افتتاحي/قيد تسويات
        public JournalType Type { get; set; }

        // المستخدم الذي أنشأ القيد
        public string CreatedBy { get; set; }

        // تاريخ الإنشاء
        public DateTime CreatedDate { get; set; }

        // قائمة بنقاط القيد
        public virtual ICollection<JournalEntry> Entries { get; set; }
    }

    public enum JournalStatus
    {
        Draft = 1,      // مسودة
        Posted = 2,     // مسجل
        Approved = 3,   // معتمد
        Cancelled = 4   // ملغى
    }

    public enum JournalType
    {
        Normal = 1,     // قيد عادي
        Opening = 2,    // قيد افتتاحي
        Adjustment = 3, // قيد تسويات
        Closing = 4     // قيد ختامي
    }
}
