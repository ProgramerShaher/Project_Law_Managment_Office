using LawOfficeManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LawOfficeManagement.Application.Features.Clients.Commands.Update
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? NationalIdImage { get; set; }
        public ClientType ClientType { get; set; }
        public int ClientRoleId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
      
    }
}
