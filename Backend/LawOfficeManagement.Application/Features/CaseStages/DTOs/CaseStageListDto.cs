using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.CaseStages.DTOs
{
    public class CaseStageListDto
    {
        public int Id { get; set; }
        public string Stage { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? EndDateStage { get; set; }
        public int CaseId { get; set; }
        public string? CaseTitle { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
