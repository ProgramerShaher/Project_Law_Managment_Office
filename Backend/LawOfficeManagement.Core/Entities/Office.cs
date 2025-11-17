using LawOfficeManagement.Core.Entities.Documents;
using System;
using System.Collections.Generic;

namespace LawOfficeManagement.Core.Entities
{
    public class Office : BaseEntity
    {
        public string OfficeName { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public string WebSitUrl { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime? EstablishmentDate { get; set; } = DateTime.Now;

        // قائمة المحامين التابعين للمكتب
        public ICollection<Lawyer>? Lawyers { get; set; }

        // قائمة الوثائق التابعة للمكتب
        public ICollection<Document>? Documents { get; set; }
    }
}
