using FluentValidation;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;

namespace LawOfficeManagement.Application.Features.CaseSessions.Validators
{
    public class UpdateCaseSessionStatusCommandValidator : AbstractValidator<UpdateCaseSessionStatusCommand>
    {
        public UpdateCaseSessionStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0");

            RuleFor(x => x.SessionStatus)
                .IsInEnum().WithMessage("Invalid session status");
        }
    }
}