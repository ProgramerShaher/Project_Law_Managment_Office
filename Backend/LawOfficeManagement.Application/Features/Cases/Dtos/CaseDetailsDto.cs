using LawOfficeManagement.Core.Entities.Cases;

public class CaseDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string? CaseNumberProsecution { get; set; }
    public string? CaseNumberInPolice { get; set; }
    public string? InternalReference { get; set; }
    public DateTime FilingDate { get; set; }
    public DateTime? FirstSessionDate { get; set; }
    public int CourtTypeId { get; set; }
    public string? CourtTypeName { get; set; }
    public int CourtId { get; set; }
    public string? CourtName { get; set; }
    public int CourtDivisionId { get; set; }
    public string? CourtDivisionName { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public int? PowerOfAttorneyId { get; set; }
    public string? PrincipalMandator { get; set; }
    public int OpponentId { get; set; }
    public string? OpponentName { get; set; }
    public CaseStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public int CaseTypeId { get; set; }
    public string? CaseTypeName { get; set; }
    public string? Notes { get; set; }
    public bool IsArchived { get; set; }
    public bool IsConfidential { get; set; }
    public string? Outcome { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}