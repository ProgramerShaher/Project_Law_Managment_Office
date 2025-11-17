using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// مراحل القضية 
    /// </summary>
    public  class CaseStage : BaseEntity
    {
        /// <summary>
        /// المرحلة
        /// </summary>
        public string Stage { get; set; }
        /// <summary>
        /// الاولوية
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// اذا تم انشاء مرحلة جديدة بعد الحالية تكن = false 
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// تايخ الانتهاء من المرحلة 
        /// </summary>
        public DateTime ? EndDateStage { get; set; }
        /// <summary>
        /// القضية 
        /// </summary>
        public int CaseId { get; set; }
        public virtual Case ?Case { get; set; } 


    }
}
