namespace LawOfficeManagement.Core.Entities.Cases
{
    public class CourtType : BaseEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public ICollection<Court> Courts { get; set; } = new List<Court>();
        public ICollection<Case> Cases { get; set; } = new List<Case>();

    }
}
