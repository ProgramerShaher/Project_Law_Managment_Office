using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    /// <summary>
    /// جدول الميزانية
    /// </summary>
    public class Budget
    {
        // رقم الميزانية (مفتاح أساسي)
        public int BudgetId { get; set; }

        // السنة المالية
        public int FiscalYear { get; set; }

        // رقم الحساب
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        // الشهر
        public int Month { get; set; }

        // المبلغ المخصص
        public decimal BudgetAmount { get; set; }

        // المبلغ الفعلي المنفق
        public decimal ActualAmount { get; set; }

        // نسبة الانحراف
        public decimal VariancePercentage { get; set; }

        // تاريخ الإنشاء
        public DateTime CreatedDate { get; set; }
    }
}
