using System;

namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// يحتوي على البيانات الاساسية لاي جدول في قاعدة البيانات لذلك كل الجداول تورث منه 
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
