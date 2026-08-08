using FluentValidation;
using SmartRecruitment.API.Models.DTOs.Employer;

namespace SmartRecruitment.API.Validators.Employer
{
    public class CreateEmployerProfileValidator
        : AbstractValidator<CreateEmployerProfileRequestDto>
    {
        public CreateEmployerProfileValidator()
        {
            RuleFor(request => request.CompanyName)
                .NotEmpty()
                .WithMessage("Company name is required.")
                .MaximumLength(150)
                .WithMessage(
                    "Company name must not exceed 150 characters.");

            RuleFor(request => request.CompanyDescription)
                .NotEmpty()
                .WithMessage("Company description is required.")
                .MaximumLength(1000)
                .WithMessage(
                    "Company description must not exceed 1000 characters.");

            RuleFor(request => request.Location)
                .NotEmpty()
                .WithMessage("Location is required.")
                .MaximumLength(150)
                .WithMessage(
                    "Location must not exceed 150 characters.");

            RuleFor(request => request.ContactNumber)
                .NotEmpty()
                .WithMessage("Contact number is required.")
                .MaximumLength(30)
                .WithMessage(
                    "Contact number must not exceed 30 characters.");

            RuleFor(request => request.Website)
                .MaximumLength(300)
                .WithMessage(
                    "Website must not exceed 300 characters.")
                .When(request =>
                    !string.IsNullOrWhiteSpace(request.Website));
        }
    }
}