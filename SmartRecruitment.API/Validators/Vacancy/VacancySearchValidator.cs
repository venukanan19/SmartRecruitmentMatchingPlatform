using FluentValidation;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Models.DTOs.Vacancy;

namespace SmartRecruitment.API.Validators.Vacancy
{
    public class VacancySearchValidator
        : AbstractValidator<VacancySearchRequestDto>
    {
        public VacancySearchValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.SearchTerm))
                .WithMessage("Search term cannot exceed 100 characters.");

            RuleFor(x => x.Location)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Location))
                .WithMessage("Location cannot exceed 100 characters.");

            RuleFor(x => x.MaxRequiredExperienceYears)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxRequiredExperienceYears.HasValue)
                .WithMessage("Experience years cannot be negative.");

            RuleFor(x => x.SkillId)
                .GreaterThan(0)
                .When(x => x.SkillId.HasValue)
                .WithMessage("SkillId must be greater than 0.");
        }
    }
}