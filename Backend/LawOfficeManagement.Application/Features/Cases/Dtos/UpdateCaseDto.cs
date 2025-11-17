using LawOfficeManagement.Core.Entities.Cases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Cases.Dtos
{
    public class UpdateCaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string? CaseNumberProsecution { get; set; }
        public string? CaseNumberInPolice { get; set; }
        public string? InternalReference { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime? FirstSessionDate { get; set; }
        public int CourtTypeId { get; set; }
        public int CourtId { get; set; }
        public int CourtDivisionId { get; set; }
        public int ClientId { get; set; }
        public int? PowerOfAttorneyId { get; set; }
        public int OpponentId { get; set; }
        public CaseStatus Status { get; set; }
        public int CaseTypeId { get; set; }
        public string? Notes { get; set; }
        public bool IsConfidential { get; set; }
        public string? Outcome { get; set; }
        public bool IsArchived { get; set; }
    }
}
