using FluentValidation;

namespace LawOfficeManagement.Application.Features.CourtTypes.Commands.UpdateCourtType
{
    public class UpdateCourtTypeCommandValidator : AbstractValidator<UpdateCourtTypeCommand>
    {
        public UpdateCourtTypeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .When(x => x.Notes != null);
        }
    }
}
