using FluentValidation;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Commands.CreatePowerOfAttorney
{
    public class CreatePowerOfAttorneyCommandValidator : AbstractValidator<CreatePowerOfAttorneyCommand>
    {
        public CreatePowerOfAttorneyCommandValidator()
        {
            RuleFor(x => x.AgencyNumber)
                .NotEmpty().WithMessage("رقم الوكالة مطلوب.");

            RuleFor(x => x.IssueDate)
                .NotEmpty().WithMessage("تاريخ الإصدار مطلوب.");

            RuleFor(x => x.IssuingAuthority)
                .NotEmpty().WithMessage("الجهة المصدرة مطلوبة.");

            RuleFor(x => x.AgencyType)
                .NotEmpty().WithMessage("نوع الوكالة مطلوب.");
        }
    }
}
