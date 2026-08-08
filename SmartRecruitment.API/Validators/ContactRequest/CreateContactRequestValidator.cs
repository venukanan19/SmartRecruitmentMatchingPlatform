using FluentValidation;
using SmartRecruitment.API.Models.DTOs.ContactRequest;

namespace SmartRecruitment.API.Validators.ContactRequest
{
    public class CreateContactRequestValidator
        : AbstractValidator<CreateContactRequestDto>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.JobSeekerProfileId)
                .GreaterThan(0)
                .WithMessage("JobSeekerProfileId must be greater than 0.");

            RuleFor(x => x.Message)
                .MaximumLength(500)
                .WithMessage("Message cannot exceed 500 characters.");
        }
    }
}