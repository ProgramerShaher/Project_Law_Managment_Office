using FluentValidation;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;

namespace LawOfficeManagement.Application.Features.CaseSessions.Validators
{
    public class UpdateCaseSessionAttendanceCommandValidator : AbstractValidator<UpdateCaseSessionAttendanceCommand>
    {
        public UpdateCaseSessionAttendanceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0");
        }
    }
}