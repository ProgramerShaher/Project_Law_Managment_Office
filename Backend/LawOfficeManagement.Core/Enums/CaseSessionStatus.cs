using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Enums
{
    /// <summary>
    /// حاله الجلسة 
    /// </summary>
    public enum CaseSessionStatus
    {
        /// <summary>
        ///  قيد التنفيذ
        /// </summary>
        Pending = 1,
        /// <summary>
        /// مؤجلة
        /// </summary>
        Postponed = 2,
        /// <summary>
        /// منتهية
        /// </summary>        
        Completed = 3,
        /// <summary>
        ///  محجوزة للحكم
        /// </summary>
        ReservedForJudgment = 4,
        /// <summary>
        /// منتهية
        /// </summary>
        Cancelled = 5   
    }
}
