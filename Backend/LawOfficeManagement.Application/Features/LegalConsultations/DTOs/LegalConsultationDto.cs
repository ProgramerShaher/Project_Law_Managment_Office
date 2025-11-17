// LawOfficeManagement.Application.Features.LegalConsultations.DTOs

namespace LawOfficeManagement.Application.Features.LegalConsultations.DTOs
{
    public class LegalConsultationDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string MobileNumber2 { get; set; }
        public string Email { get; set; }
        public int LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string ConsultationType { get; set; }
        public int ServiceOfficeId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Notes { get; set; }
        public string UrlLegalConsultation { get; set; }
        public string UrlLegalConsultationInvoice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateLegalConsultationDto
    {
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string MobileNumber2 { get; set; }
        public string Email { get; set; }
        public int LawyerId { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string ConsultationType { get; set; }
        public int ServiceOfficeId { get; set; }
        public string Notes { get; set; }
        public string UrlLegalConsultation { get; set; }
        public string UrlLegalConsultationInvoice { get; set; }
        public string Status { get; set; }
    }

    public class UpdateLegalConsultationDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string MobileNumber2 { get; set; }
        public string Email { get; set; }
        public int LawyerId { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string ConsultationType { get; set; }
        public int ServiceOfficeId { get; set; }
        public string Notes { get; set; }
        public string UrlLegalConsultation { get; set; }
        public string UrlLegalConsultationInvoice { get; set; }
        public string Status { get; set; }
    }
}