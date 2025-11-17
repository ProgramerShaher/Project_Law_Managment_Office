using FluentValidation;

namespace LawOfficeManagement.Application.Features.Clients.Commands.Update
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("اسم العميل مطلوب.")
                .MaximumLength(100).WithMessage("يجب ألا يتجاوز اسم العميل 100 حرف.");
            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب.")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي.");
            RuleFor(x => x.ClientRoleId)
                .GreaterThan(0).WithMessage("دور العميل مطلوب.");
        }
    }
}
