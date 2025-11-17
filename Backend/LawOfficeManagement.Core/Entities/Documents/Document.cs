using LawOfficeManagement.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace LawOfficeManagement.Core.Entities.Documents
{
    public class Document : BaseEntity
    {
        public string DocumentName { get; set; }

        public string FileExtension { get; set; }

        public string FileUrl { get; set; }

        public DocumentType DocumentType { get; set; } // Enum

        // العلاقة العامة (قد تكون الوثيقة تابعة لعميل أو محامي أو قضية...الخ)
        public int EntityId { get; set; }

        public EntityOwnerType EntityOwnerType { get; set; } // Enum لتحديد نوع الكيان التابع له
    }
}
