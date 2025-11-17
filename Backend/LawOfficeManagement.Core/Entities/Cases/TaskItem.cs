using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Documents;
using LawOfficeManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.Core.Entities.Cases
{
    public class TaskItem : BaseEntity
    {
        // 🧩 معلومات أساسية
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;       // عنوان المهمة
         /// <summary>
         ///وصف تفصيلي للمهمة
         /// </summary>
        [MaxLength(2000)]
        public string? Description { get; set; }               

        // 🕒 المواعيد
        public DateTime? StartDate { get; set; }                // بداية التنفيذ
        public DateTime? DueDate { get; set; }                  // الموعد النهائي
        public DateTime? CompletedAt { get; set; }              // تاريخ الإنهاء إن وجِد

        // ⚙️ الحالة
        public TaskStatu Status { get; set; } = TaskStatu.Pending;  // حالة المهمة (قيد الانتظار، منجزة، مؤجلة...)
        public TaskPriority Priority { get; set; } = TaskPriority.Normal; // أولوية المهمة

        /// <summary>
        /// من أنشأ المهمة
        /// </summary>
        public int? CreatedById { get; set; }                   
        public virtual Lawyer? CreatedBy { get; set; }

        /// <summary>
        /// عضو من المكلفين بالقضية
        /// </summary>
        public int? CaseTeamId { get; set; }                   
        public virtual CaseTeam? CaseTeam { get; set; }

        public int? CaseId { get; set; }                   
        public virtual Case? Case { get; set; }

        // 💬 الملاحظات أو التحديثات
        public virtual ICollection<TaskComment>? Comments { get; set; }

        /// المرفقات
        public virtual ICollection<TaskDocument>? Documents { get; set; }
    }
    /// <summary>
    /// تعليقات على المهام
    /// </summary>
    public class TaskComment : BaseEntity
    {
        [Required]
        public int TaskItemId { get; set; }
        public virtual TaskItem TaskItem { get; set; }

        [Required, MaxLength(1000)]
        public string Content { get; set; } = string.Empty; // نص التعليق

        public int CreatedById { get; set; }
        public virtual Lawyer CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}