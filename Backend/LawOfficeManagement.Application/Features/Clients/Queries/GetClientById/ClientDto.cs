using LawOfficeManagement.Core.Enums;

namespace LawOfficeManagement.Application.Features.Clients.Queries.GetClientById
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? UrlImageNationalId { get; set; }
        public ClientType ClientType { get; set; }
        public int ClientRoleId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
