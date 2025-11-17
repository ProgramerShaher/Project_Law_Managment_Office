using FluentValidation;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;

namespace LawOfficeManagement.Application.Features.CaseSessions.Validators
{
    public class UpdateCaseSessionDecisionCommandValidator : AbstractValidator<UpdateCaseSessionDecisionCommand>
    {
        public UpdateCaseSessionDecisionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0");

            RuleFor(x => x.Decision)
                .MaximumLength(2000).WithMessage("Decision cannot exceed 2000 characters")
                .When(x => !string.IsNullOrEmpty(x.Decision));

            RuleFor(x => x.NextSessionDate)
                .GreaterThan(DateTime.Now).WithMessage("Next session date must be in the future")
                .When(x => x.NextSessionDate.HasValue);
        }
    }
}