using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Enums
{
    /// <summary>
    /// حالة المهمة
    /// </summary>
    public enum TaskStatu
    {
        Pending = 0,     // لم تبدأ
        InProgress = 1,  // قيد التنفيذ
        Completed = 2,   // منجزة
        Delayed = 3,     // متأخرة
        Cancelled = 4    // ملغاة
    }
    /// <summary>
    /// اولوية المهمة
    /// </summary>
    public enum TaskPriority
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Urgent = 3
    }

}
