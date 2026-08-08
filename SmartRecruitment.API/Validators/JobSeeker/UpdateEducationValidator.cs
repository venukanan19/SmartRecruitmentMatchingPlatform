using FluentValidation;
using SmartRecruitment.API.Models.DTOs.JobSeeker;

namespace SmartRecruitment.API.Validators.JobSeeker
{
    public class UpdateEducationValidator
    : AbstractValidator<UpdateEducationRequestDto>
    {
        public UpdateEducationValidator()
        {
            RuleFor(x => x.InstitutionName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Qualification)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.FieldOfStudy)
                .MaximumLength(150);

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue);
        }
    }
}
