using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    /// <summary>
    /// جدول التقارير المالية
    /// </summary>
    public class FinancialReport
    {
        // رقم التقرير (مفتاح أساسي)
        public int ReportId { get; set; }

        // نوع التقرير: ميزان مراجعة/قائمة دخل/قائمة مركز مالي
        public ReportType Type { get; set; }

        // تاريخ بداية الفترة
        public DateTime StartDate { get; set; }

        // تاريخ نهاية الفترة
        public DateTime EndDate { get; set; }

        // بيانات التقرير (JSON أو XML)
        public string ReportData { get; set; }

        // تاريخ إنشاء التقرير
        public DateTime GeneratedDate { get; set; }

        // المستخدم الذي أنشأ التقرير
        public string GeneratedBy { get; set; }
    }

    public enum ReportType
    {
        TrialBalance = 1,       // ميزان المراجعة
        IncomeStatement = 2,    // قائمة الدخل
        BalanceSheet = 3,       // قائمة المركز المالي
        CashFlow = 4           // قائمة التدفقات النقدية
    }
}
