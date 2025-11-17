using FluentValidation;

namespace LawOfficeManagement.Application.Features.Courts.Commands.UpdateCourt
{
    public class UpdateCaseTypeCommandValidator : AbstractValidator<UpdateCourtCommand>
    {
        public UpdateCaseTypeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CourtTypeId).GreaterThan(0);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(300);
            RuleForEach(x => x.Divisions).ChildRules(div =>
            {
                div.RuleFor(d => d.Name).NotEmpty().MaximumLength(200);
                div.RuleFor(d => d.JudgeName).NotEmpty().MaximumLength(150);
            });
        }
    }
}
