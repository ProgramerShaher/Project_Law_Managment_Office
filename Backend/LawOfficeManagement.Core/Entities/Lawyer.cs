using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Finance;
using LawOfficeManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LawOfficeManagement.Core.Entities
{
    /// <summary>
    /// يمثل بيانات المحامي داخل نظام إدارة مكتب المحاماة.
    /// </summary>
    public class Lawyer : BaseEntity
    {
        /// <summary>
        /// الاسم الكامل للمحامي.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// رقم الهاتف الخاص بالمحامي للتواصل.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// عنوان السكن أو المكتب للمحامي.
        /// </summary>
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// مسار صورة الهوية الوطنية أو جواز السفر.
        /// </summary>
        [MaxLength(300)]
        public string IdentityImagePath { get; set; }

        /// <summary>
        /// مسار المجلد أو الملفات الخاصة بالمؤهلات العلمية.
        /// </summary>
        [MaxLength(300)]
        public string QualificationDocumentsPath { get; set; }

        /// <summary>
        /// البريد الإلكتروني للمحامي.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// تاريخ ميلاد المحامي.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

       

        /// <summary>
        /// نوع المحامي (متدرب، مستشار، خبير...).
        /// </summary>
        public LawyerType Type { get; set; }
        public virtual ICollection<CaseTeam> CaseTeams { get; set; }
        public virtual ICollection<DerivedPowerOfAttorney> DerivedPowerOfAttorney { get; set; } 
        public virtual ICollection<LegalConsultation> LegalConsultations { get; set; }

        public virtual ICollection<CaseSession> CaseSessions { get; set; }
    = new List<CaseSession>();

        /// <summary>
        ///  الرصيد الافتتاحي
        /// </summary>
        //  public decimal OpeningBalance { get; set; }
        /// <summary>
        /// الحساب المرتبط في دليل الحسابات
        /// </summary>
        //  public int AccountId { get; set; }
        // public virtual Account Account { get; set; }
    }
}
