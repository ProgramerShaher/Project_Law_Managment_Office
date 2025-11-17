using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.CaseStages.DTOs
{
    public class CreateCaseStageDto
    {
        public string Stage { get; set; }
        public string Priority { get; set; } 
        public int CaseId { get; set; }
    }
}
