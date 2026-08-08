using FluentValidation;
using SmartRecruitment.API.Models.DTOs.Vacancy;

namespace SmartRecruitment.API.Validators.Vacancy
{
    public class CreateVacancyValidator
        : AbstractValidator<CreateVacancyRequestDto>
    {
        public CreateVacancyValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(200)
                .WithMessage(
                    "Title must not exceed 200 characters.");

            RuleFor(request => request.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(2000)
                .WithMessage(
                    "Description must not exceed 2000 characters.");

            RuleFor(request => request.Location)
                .NotEmpty()
                .WithMessage("Location is required.")
                .MaximumLength(150)
                .WithMessage(
                    "Location must not exceed 150 characters.");

            RuleFor(request => request.RequiredExperienceYears)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "Required experience years cannot be negative.");

            RuleFor(request => request.EducationRequirement)
                .NotEmpty()
                .WithMessage("Education requirement is required.")
                .MaximumLength(300)
                .WithMessage(
                    "Education requirement must not exceed 300 characters.");

            RuleFor(request => request.RequiredSkillIds)
                .NotEmpty()
                .WithMessage(
                    "At least one required skill must be selected.");

            RuleForEach(request => request.RequiredSkillIds)
                .GreaterThan(0)
                .WithMessage(
                    "Required skill IDs must be valid.");
        }
    }
}