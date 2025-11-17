// LawOfficeManagement.Application.Features.ServiceOffices.DTOs

namespace LawOfficeManagement.Application.Features.ServiceOffices.DTOs
{
    public class ServiceOfficeDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string? Notes { get; set; }
        public int ConsultationCount { get; set; }
    }

    public class CreateServiceOfficeDto
    {
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateServiceOfficeDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Notes { get; set; }
    }
}