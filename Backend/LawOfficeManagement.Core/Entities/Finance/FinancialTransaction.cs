using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    /// <summary>
    /// جدول الحركة المالية
    /// </summary>
    public class FinancialTransaction
    {
        // رقم الحركة (مفتاح أساسي)
        public int TransactionId { get; set; }

        // رقم الحركة التسلسلي
        public string TransactionNumber { get; set; }

        // تاريخ الحركة
        public DateTime TransactionDate { get; set; }

        // نوع الحركة: سداد/تحصيل/صرف
        public TransactionType Type { get; set; }

        // المبلغ
        public decimal Amount { get; set; }

        // طريقة السداد: نقدي/شيك/تحويل
        public PaymentMethod PaymentMethod { get; set; }

        // رقم المرجع (رقم الشيك أو التحويل)
        public string ReferenceNumber { get; set; }

        // الوصف
        public string Description { get; set; }

        // العميل/المورد المرتبط
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int? OfficeId { get; set; }
        public virtual Office Office { get; set; }

        // رقم القيد المحاسبي
        public int? JournalId { get; set; }
        public virtual Journal Journal { get; set; }

        // حالة الحركة
        public TransactionStatus Status { get; set; }

        // تاريخ الإنشاء
        public DateTime CreatedDate { get; set; }
    }

    public enum TransactionType
    {
        Payment = 1,    // سداد
        Receipt = 2,    // تحصيل
        Expense = 3     // صرف
    }

    public enum PaymentMethod
    {
        Cash = 1,       // نقدي
        Check = 2,      // شيك
        Transfer = 3    // تحويل
    }

    public enum TransactionStatus
    {
        Pending = 1,    // معلق
        Completed = 2,  // مكتمل
        Cancelled = 3   // ملغى
    }
}
