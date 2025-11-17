// LawOfficeManagement.Application.Features.Opponents.DTOs
using LawOfficeManagement.Core.Entities;

namespace LawOfficeManagement.Application.Features.Opponents.DTOs
{
    public class OpponentDto
    {
        public int Id { get; set; }
        public string OpponentName { get; set; }
        public string OpponentMobile { get; set; }
        public string OpponentAddress { get; set; }
        public OpponentType Type { get; set; }
        public string TypeName { get; set; }
        public string OpponentLawyer { get; set; }
        public int CasesCount { get; set; }
    }

    public class CreateOpponentDto
    {
        public string OpponentName { get; set; }
        public string OpponentMobile { get; set; }
        public string OpponentAddress { get; set; }
        public OpponentType Type { get; set; }
        public string OpponentLawyer { get; set; }
    }

    public class UpdateOpponentDto
    {
        public int Id { get; set; }
        public string OpponentName { get; set; }
        public string OpponentMobile { get; set; }
        public string OpponentAddress { get; set; }
        public OpponentType Type { get; set; }
        public string OpponentLawyer { get; set; }
    }

    public class OpponentCaseDto
    {
        public int Id { get; set; }
        public string OpponentName { get; set; }
        public string OpponentMobile { get; set; }
        public string TypeName { get; set; }
        public List<OpponentCaseInfoDto> Cases { get; set; } = new();
    }

    public class OpponentCaseInfoDto
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string CaseType { get; set; }
        public string Status { get; set; }
    }
}