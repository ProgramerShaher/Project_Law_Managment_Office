namespace LawOfficeManagement.Core.Entities.Cases
{
    public class Court : BaseEntity
    {
        public string Name { get; set; }
        public int CourtTypeId { get; set; }
        public CourtType CourtType { get; set; }
        public string Address { get; set; }
        public ICollection<CourtDivision> Divisions { get; set; } = new List<CourtDivision>();
        public ICollection<Case> Cases { get; set; } = new List<Case>();
        public virtual ICollection<CaseSession> CaseSessions { get; set; }

    }
}
