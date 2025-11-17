using FluentValidation;
using LawOfficeManagement.Application.Features.Cases.Commands.Dtos;

namespace LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseSession
{
    public class CreateCaseSessionCommandValidator : AbstractValidator<CreateCaseSessionCommand>
    {
        public CreateCaseSessionCommandValidator()
        {
            RuleFor(x => x.CreateCaseSessionDto.CaseId)
                .GreaterThan(0).When(x => x.CreateCaseSessionDto.CaseId.HasValue)
                .WithMessage("Case ID must be greater than 0");

            RuleFor(x => x.CreateCaseSessionDto.CourtId)
                .GreaterThan(0).WithMessage("Court ID is required");

            RuleFor(x => x.CreateCaseSessionDto.CourtDivisionId)
                .GreaterThan(0).WithMessage("Court Division ID is required");

            RuleFor(x => x.CreateCaseSessionDto.SessionDate)
                .NotEmpty().WithMessage("Session date is required")
                .GreaterThan(DateTime.Now.AddDays(-1)).WithMessage("Session date cannot be in the past");

            RuleFor(x => x.CreateCaseSessionDto.SessionType)
                .MaximumLength(50).WithMessage("Session type cannot exceed 50 characters");

            RuleForEach(x => x.CreateCaseSessionDto.Evidences)
                .SetValidator(new CreateCaseEvidenceDtoValidator());

            RuleForEach(x => x.CreateCaseSessionDto.Witnesses)
                .SetValidator(new CreateCaseWitnessDtoValidator());
        }
    }

    public class CreateCaseEvidenceDtoValidator : AbstractValidator<CreateCaseEvidenceDto>
    {
        public CreateCaseEvidenceDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Evidence title is required")
                .MaximumLength(200).WithMessage("Evidence title cannot exceed 200 characters");
        }
    }

    public class CreateCaseWitnessDtoValidator : AbstractValidator<CreateCaseWitnessDto>
    {
        public CreateCaseWitnessDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Witness full name is required")
                .MaximumLength(200).WithMessage("Witness name cannot exceed 200 characters");
        }
    }
}