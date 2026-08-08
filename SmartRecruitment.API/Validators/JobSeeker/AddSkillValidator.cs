using FluentValidation;
using SmartRecruitment.API.Models.DTOs.JobSeeker;

namespace SmartRecruitment.API.Validators.JobSeeker
{
    public class AddSkillValidator
     : AbstractValidator<AddJobSeekerSkillRequestDto>
    {
        public AddSkillValidator()
        {
            RuleFor(x => x.SkillId)
                .GreaterThan(0);
 
        }
    }
}
