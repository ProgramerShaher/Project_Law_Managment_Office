using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.CaseTeams.DTOs
{
    public class CaseTeamLawyerDto
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string CaseTitle { get; set; } = string.Empty;
        public string CaseNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string CaseStatus { get; set; } = string.Empty;
    }
}
