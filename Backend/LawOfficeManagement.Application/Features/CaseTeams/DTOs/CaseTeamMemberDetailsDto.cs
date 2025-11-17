using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.CaseTeams.DTOs
{
    public class CaseTeamMemberDetailsDto
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public string LawyerEmail { get; set; } = string.Empty;
        public string LawyerPhone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
      
    }
}
