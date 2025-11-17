using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Finance
{
    public class Bill_Item : BaseEntity
    {
        [ForeignKey("Bill")]
        public int Bill_Id { get; set; }  // ربط البيانات بالقضية
        public virtual Bill Bill { get; set; } 
        public string Details { get; set; } 
        public double TheAmount { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; } = "SAR"; // العملة


    }
}
