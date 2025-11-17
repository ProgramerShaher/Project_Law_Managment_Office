using LawOfficeManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// ÕæÑÉ ÇáåæíÉ ÇáæØäíÉ ááãÍÇãí (íÊã ÑİÚåÇ ÚÈÑ IFormFile)
        /// </summary>
        public string IdentityImagePath { get; set; }
        /// <summary>
        /// ãÓÇÑ ÇáãáİÇÊ Ãæ ÕæÑ ÇáãÄåáÇÊ ÇáÚáãíÉ (ÇÎÊíÇÑí)
        /// </summary>
        public string QualificationDocumentsPath { get; set; }

        public string  ?Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public LawyerType Type { get; set; } // enum 
    }
}
