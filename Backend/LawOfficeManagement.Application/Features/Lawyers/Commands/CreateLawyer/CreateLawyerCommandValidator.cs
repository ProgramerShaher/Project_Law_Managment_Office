using FluentValidation;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateCaseCommandHandler : AbstractValidator<CreateLawyerCommand>
    {
        public CreateCaseCommandHandler()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("«·«”„ «·ﬂ«„· „ÿ·Ê»")
                .MaximumLength(200);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("—ﬁ„ «·Â« › „ÿ·Ê»")
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("«·»—Ìœ «·≈·ﬂ —Ê‰Ì „ÿ·Ê»")
                .EmailAddress().WithMessage("»—Ìœ ≈·ﬂ —Ê‰Ì €Ì— ’«·Õ");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("‰Ê⁄ «·„Õ«„Ì €Ì— ’«·Õ");
        }
    }
}
