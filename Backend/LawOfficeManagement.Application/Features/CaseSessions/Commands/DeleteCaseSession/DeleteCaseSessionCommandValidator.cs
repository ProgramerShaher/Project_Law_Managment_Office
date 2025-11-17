using FluentValidation;
using LawOfficeManagement.Application.Features.CaseSessions.Commands;

namespace LawOfficeManagement.Application.Features.CaseSessions.Validators
{
    public class DeleteCaseSessionCommandValidator : AbstractValidator<DeleteCaseSessionCommand>
    {
        public DeleteCaseSessionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0");
        }
    }
}