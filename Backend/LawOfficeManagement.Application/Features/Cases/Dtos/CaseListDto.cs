using LawOfficeManagement.Core.Entities.Cases;

public class CaseListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CaseNumber { get; set; } = string.Empty;
    public string? InternalReference { get; set; }
    public DateTime FilingDate { get; set; }
    public string? ClientName { get; set; }
    public string? CourtName { get; set; }
    public CaseStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public string? CaseTypeName { get; set; }
    public bool IsArchived { get; set; }
    public string? Outcome { get; set; }
}
