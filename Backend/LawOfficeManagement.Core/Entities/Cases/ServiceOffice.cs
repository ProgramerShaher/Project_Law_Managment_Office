using System;

namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// الخدمات والأسعار
    /// </summary>
    public class ServiceOffice : BaseEntity
    {
        /// <summary>
        /// اسم الخدمة
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// سعر الخدمة
        /// </summary>
        public decimal ServicePrice { get; set; }

        /// <summary>
        /// ملاحظات
        /// </summary>
        public string ?Notes { get; set; }

        public ICollection<LegalConsultation>? legalConsultations { get; set; }
    }
}
