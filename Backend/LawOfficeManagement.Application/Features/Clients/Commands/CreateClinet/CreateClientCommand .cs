using LawOfficeManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;
using System.IO;

namespace LawOfficeManagement.Application.Features.Clients.Commands.CreateClient
{
    // الأمر نفسه هو كائن الطلب، ويحمل كل البيانات اللازمة
    public class CreateClientCommand : IRequest<int> // سيقوم بإرجاع ID العميل الجديد
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UrlImageNationalId { get; set; } // صورة الهوية الوطنية
                      
        public ClientType ClientType { get; set; }
        public int ClientRoleId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // بيانات العنوان (Value Object)
        //    public string Country { get; set; }
        //    public string Street { get; set; }
        //    public string City { get; set; }
        //    public string PostalCode { get; set; }
        //}
    }
}