namespace LawOfficeManagement.Application.Features.Courts.DTOs
{
    public class CourtDivisionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JudgeName { get; set; }
    }

    public class CourtDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtTypeName { get; set; }
        public string Address { get; set; }
        public List<CourtDivisionDto> Divisions { get; set; } = new();
    }
}
