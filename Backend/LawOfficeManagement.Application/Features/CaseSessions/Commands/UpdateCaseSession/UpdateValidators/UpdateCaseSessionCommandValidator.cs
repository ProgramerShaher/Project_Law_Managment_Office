using FluentValidation;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Validators
{
    public class UpdateCaseSessionCommandValidator : AbstractValidator<UpdateCaseSessionCommand>
    {
        public UpdateCaseSessionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0");

            RuleFor(x => x.UpdateCaseSessionDto.CourtId)
                .GreaterThan(0).WithMessage("Court ID is required");

            RuleFor(x => x.UpdateCaseSessionDto.CourtDivisionId)
                .GreaterThan(0).WithMessage("Court Division ID is required");

            RuleFor(x => x.UpdateCaseSessionDto.SessionDate)
                .NotEmpty().WithMessage("Session date is required")
                .GreaterThan(DateTime.Now.AddDays(-1)).WithMessage("Session date cannot be in the past");

            RuleFor(x => x.UpdateCaseSessionDto.SessionType)
                .MaximumLength(50).WithMessage("Session type cannot exceed 50 characters");

            RuleFor(x => x.UpdateCaseSessionDto.SessionNumber)
                .MaximumLength(100).WithMessage("Session number cannot exceed 100 characters");

            RuleFor(x => x.UpdateCaseSessionDto.Location)
                .MaximumLength(500).WithMessage("Location cannot exceed 500 characters");

            RuleFor(x => x.UpdateCaseSessionDto.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

            RuleFor(x => x.UpdateCaseSessionDto.Decision)
                .MaximumLength(2000).WithMessage("Decision cannot exceed 2000 characters");
        }
    }
}