using System;
using FluentValidation;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Update
{
    public class UpdateOfficeCommandValidator : AbstractValidator<UpdateOfficeCommand>
    {
        public UpdateOfficeCommandValidator()
        {
            RuleFor(x => x.OfficeName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.ManagerName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.WebSitUrl)
                .Must(url => string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("رابط الموقع غير صالح");

            RuleFor(x => x.LicenseNumber)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.LicenseNumber));
        }
    }
}
