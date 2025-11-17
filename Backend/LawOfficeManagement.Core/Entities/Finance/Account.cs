using LawOfficeManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    public class Account
    {
        /// <summary>
        /// الرقم الرئيسي للحساب (مفتاح أساسي)
        /// </summary>
        public int AccountId { get; set; }

        // رقم الحساب حسب التصنيف المحاسبي (مثل 101، 102، إلخ)
        public string AccountNumber { get; set; }

        // اسم الحساب بالعربية (مثل "النقدية"، "البنك")
        public string AccountNameAr { get; set; }

        // اسم الحساب بالإنجليزية
      //  public string AccountNameEn { get; set; }

        /// <summary>
        /// نوع الحساب: 1=أصول، 2=خصوم، 3=إيرادات، 4=مصروفات
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// مستوى الحساب في الشجرة المحاسبية
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// الرصيد الافتتاحي للحساب
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// عملة الحساب
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// حالة الحساب (نشط/غير نشط)
        /// </summary>
        public bool IsActive { get; set; }

        // تاريخ إنشاء الحساب
     //   public DateTime CreatedDate { get; set; }

        // الحساب الرئيسي (لحسابات التفصيل)
        public int? ParentAccountId { get; set; }
        public virtual Account ParentAccount { get; set; }
    }

   
}
