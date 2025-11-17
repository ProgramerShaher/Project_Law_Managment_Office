namespace LawOfficeManagement.Application.Features.Clients.Queries.GetAllClients
{
    public class ClientSummaryDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ClientTypeName { get; set; }
        public string ClientRoleName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
