//using FluentValidation;
//using LawOfficeManagement.Application.Features.Courts.Commands.UpdateCourt;

//namespace LawOfficeManagement.Application.Features.CaseTypes.Commands.UpdateCaseType
//{
//    public class UpdateCaseTypeCommandValidator : AbstractValidator<UpdateCaseTypeCommandValidator>
//    {
//        public UpdateCaseTypeCommandValidator()
//        {
//            RuleFor(x => x.des).NotEmpty().MaximumLength(200);
//            RuleFor(x => x.CourtTypeId).GreaterThan(0);
//            RuleFor(x => x.).NotEmpty().MaximumLength(300);
//            RuleForEach(x => x.Divisions).ChildRules(div =>
//            {
//                div.RuleFor(d => d.Name).NotEmpty().MaximumLength(200);
//                div.RuleFor(d => d.JudgeName).NotEmpty().MaximumLength(150);
//            });
//        }
//    }
//}
