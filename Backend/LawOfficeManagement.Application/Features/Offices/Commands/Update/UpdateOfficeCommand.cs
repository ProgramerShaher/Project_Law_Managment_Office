using MediatR;
using System;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Update
{
    /// <summary>
    /// أمر لتحديث بيانات المكتب (Office)
    /// </summary>
    public class UpdateOfficeCommand : IRequest<bool>
    {
       

        /// <summary>
        /// اسم المكتب
        /// </summary>
        public string OfficeName { get; set; } = string.Empty;

        /// <summary>
        /// اسم المدير
        /// </summary>
        public string ManagerName { get; set; } = string.Empty;

        /// <summary>
        /// عنوان المكتب
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// الموقع الإلكتروني للمكتب
        /// </summary>
        public string? WebSitUrl { get; set; }

        /// <summary>
        /// رقم الهاتف
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// البريد الإلكتروني
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// رقم الترخيص
        /// </summary>
        public string? LicenseNumber { get; set; }

       
    }
}
