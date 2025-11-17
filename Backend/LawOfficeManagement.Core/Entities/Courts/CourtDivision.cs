namespace LawOfficeManagement.Core.Entities.Cases
{
    public class CourtDivision : BaseEntity
    {
        public int CourtId { get; set; }
        public Court Court { get; set; }
        public string Name { get; set; }
        public string JudgeName { get; set; }
        public ICollection<Case> Cases { get; set; } = new List<Case>();
        public virtual ICollection<CaseSession> CaseSessions { get; set; } = new List<CaseSession>(); // ? Ужн хах
        


    }
}
