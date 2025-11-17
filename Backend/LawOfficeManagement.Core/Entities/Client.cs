using LawOfficeManagement.Core.Common;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Finance;
using LawOfficeManagement.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawOfficeManagement.Core.Entities
{
    public class Client : BaseEntity
    {
        public string? FullName { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public DateTime BirthDate { get; set; }

        public string? UrlImageNationalId { get; set; }

        public ClientType ClientType { get; set; }

        // العلاقة مع ClientRole
        public int ClientRoleId { get; set; }
        public ClientRole? ClientRole { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }


        // Value Object
        public string? Address { get; set; }
        public ICollection<Case> Cases { get; set; }
        public ICollection<PowerOfAttorney> powerOfAttorneys { get; set; }
    //    public ICollection<AgentClient> agentClients { get; set; } = new List<AgentClient>();

        /// <summary>
        ///  الرصيد الافتتاحي
        /// </summary>
     //   public decimal OpeningBalance { get; set; }
        /// <summary>
        /// الحساب المرتبط في دليل الحسابات
        /// </summary>
        //public int AccountId { get; set; }
        //public virtual Account Account { get; set; }

    }
}
