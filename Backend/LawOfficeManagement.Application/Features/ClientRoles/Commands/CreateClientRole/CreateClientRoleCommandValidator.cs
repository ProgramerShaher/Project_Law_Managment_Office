using FluentValidation;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.CreateClientRole
{
    public class CreateClientRoleCommandValidator : AbstractValidator<CreateClientRoleCommand>
    {
        public CreateClientRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الدور مطلوب.")
                .MaximumLength(100).WithMessage("يجب ألا يتجاوز اسم الدور 100 حرف.");
        }
    }
}
