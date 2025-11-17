using FluentValidation;

namespace LawOfficeManagement.Application.Features.ClientRoles.Commands.UpdateClientRole
{
    public class UpdateClientRoleCommandValidator : AbstractValidator<UpdateClientRoleCommand>
    {
        public UpdateClientRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الدور مطلوب.")
                .MaximumLength(100).WithMessage("يجب ألا يتجاوز اسم الدور 100 حرف.");
        }
    }
}
