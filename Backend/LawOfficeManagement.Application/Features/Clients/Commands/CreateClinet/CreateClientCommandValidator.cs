using FluentValidation;

namespace LawOfficeManagement.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("اسم العميل مطلوب.")
                .MaximumLength(100).WithMessage("يجب ألا يتجاوز اسم العميل 100 حرف.");

            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.")
                .When(p => !string.IsNullOrEmpty(p.Email)); // التحقق فقط إذا لم يكن فارغًا

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب.");

            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب.")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي.");

            RuleFor(p => p.ClientRoleId)
                .GreaterThan(0).WithMessage("صفة العميل مطلوب.");
        }
    }
}
