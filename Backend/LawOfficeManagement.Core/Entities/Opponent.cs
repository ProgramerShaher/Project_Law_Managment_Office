using LawOfficeManagement.Core.Entities.Cases;

namespace LawOfficeManagement.Core.Entities
{
    public class Opponent : BaseEntity
    {
        /// <summary>
        /// اسم الخصم 
        /// </summary>
        public string? OpponentName { get; set; }

        /// <summary>
        /// رقم جوال الخصم 
        /// </summary>
        public string? OpponentMobile { get; set; } 


        /// <summary>
        /// عنوان الخصم 
        /// </summary>
        public string? OpponentAddress { get; set; }

        /// <summary>
        /// نوع الخصم 
        /// </summary>
        public OpponentType Type { get; set; } 

        /// <summary>
        /// محامي الخصم 
        /// </summary>
        public string? OpponentLawyer { get; set; }
        public ICollection<Case> cases { get; set; } 

    }

    public enum OpponentType
    {
        Individual = 1,
        Company = 2,
        Person = 3  
    }
}