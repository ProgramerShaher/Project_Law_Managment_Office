using FluentValidation;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.CreateCourtType
{
    public class CreateCourtTypeCommandValidator : AbstractValidator<CreateCourtTypeCommand>
    {
        public CreateCourtTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .When(x => x.Notes != null);
        }
    }
}
