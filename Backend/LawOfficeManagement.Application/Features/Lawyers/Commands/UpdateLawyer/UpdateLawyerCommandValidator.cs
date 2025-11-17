using FluentValidation;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.UpdateLawyer
{
    public class UpdateLawyerCommandValidator : AbstractValidator<UpdateLawyerCommand>
    {
        public UpdateLawyerCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        }
    }
}
