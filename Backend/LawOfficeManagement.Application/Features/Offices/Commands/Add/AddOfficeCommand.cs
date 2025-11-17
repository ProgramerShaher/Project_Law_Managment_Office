using MediatR;
using System;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Add
{
    public class AddOfficeCommand : IRequest<int> // يعيد Id المكتب
    {
        public string OfficeName { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public string WebSitUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
    }
}
