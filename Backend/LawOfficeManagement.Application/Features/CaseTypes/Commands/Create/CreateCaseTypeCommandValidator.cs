using FluentValidation;

namespace LawOfficeManagement.Application.Features.CaseTypes.Commands.CreateCaseType
{
    public class CreateCaseTypeCommandValidator : AbstractValidator<CreateCaseTypeCommand>
    {
        public CreateCaseTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(300);
        }
    }
}
