// CaseTypeDto

namespace LawOfficeManagement.Application.Features.CaseTypes.Queries.GetAllCaseTypes
{
    public class CaseTypeDto
    {
       
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
        
    }
}

