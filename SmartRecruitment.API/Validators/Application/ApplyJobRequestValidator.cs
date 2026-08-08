using FluentValidation;
using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Validators.Application
{
    public class ApplyJobRequestValidator
        : AbstractValidator<ApplyJobRequestDto>
    {
        public ApplyJobRequestValidator()
        {
            RuleFor(x => x.VacancyId)
                .GreaterThan(0)
                .WithMessage("VacancyId must be greater than 0.");

            RuleFor(x => x.CoverLetter)
                .MaximumLength(1000)
                .WithMessage("Cover letter cannot exceed 2000 characters.");

            RuleFor(x => x.CoverLetter)
                .Must(x => x == null || !string.IsNullOrWhiteSpace(x))
                .WithMessage("Cover letter cannot contain only whitespace.");
        }
    }
}