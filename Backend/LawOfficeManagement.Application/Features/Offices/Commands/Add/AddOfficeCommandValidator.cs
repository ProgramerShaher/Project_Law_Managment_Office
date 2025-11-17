using System;
using FluentValidation;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Add
{
    public class AddOfficeCommandValidator : AbstractValidator<AddOfficeCommand>
    {
        public AddOfficeCommandValidator()
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
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.WebSitUrl)
                .Must(url => string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("رابط الموقع غير صالح");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
