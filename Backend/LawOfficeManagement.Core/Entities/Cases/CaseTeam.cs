using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities.Cases
{
    public class CaseTeam : BaseEntity 
    {
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }

        public int CaseId { get; set; }
        public virtual Case Case { get; set; }
        /// <summary>
        /// دور المحامي في القضية (مدير، مساعد، مراجع، إلخ)
        /// </summary>
        public string Role { get; set; } = "مساعد";
 
        /// <summary>
        /// تاريخ بدء المشاركة في القضية
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// تاريخ انتهاء المشاركة في القضية
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// هل المحامي نشط حالياً في القضية؟
        /// </summary>
        public bool IsActive { get; set; } = true;
        public virtual ICollection<TaskItem> TaskItems { get; set; }
        = new List<TaskItem>();
      
    }
}