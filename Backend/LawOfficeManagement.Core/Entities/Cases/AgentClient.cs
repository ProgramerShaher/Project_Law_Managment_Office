using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Core.Entities.Cases
{
    /// <summary>
    /// 
    /// </summary>
    public  class AgentClient :BaseEntity
    {
        /// <summary>
        /// العميل 
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// الوكالة 
        /// </summary>
        public int PowerOfAttorneyId{ get; set; }


    }
}
