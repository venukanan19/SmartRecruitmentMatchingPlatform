//using SmartRecruitment.API.Models.DTOs.JobSeeker;

//namespace SmartRecruitment.API.Validators.JobSeeker
//{
//    public class UpdateExperienceValidator
//     : AbstractValidator<UpdateExperienceRequestDto>
//    {
//        public UpdateExperienceValidator()
//        {
//            RuleFor(x => x.JobTitle)
//                .NotEmpty()
//                .MaximumLength(150);

//            RuleFor(x => x.CompanyName)
//                .NotEmpty()
//                .MaximumLength(200);

//            RuleFor(x => x.Description)
//                .MaximumLength(2000);

//            RuleFor(x => x.StartDate)
//                .NotEmpty();

//            RuleFor(x => x.EndDate)
//                .GreaterThanOrEqualTo(x => x.StartDate)
//                .When(x =>
//                    !x.IsCurrent &&
//                    x.EndDate.HasValue);

//            RuleFor(x => x.EndDate)
//                .Null()
//                .When(x => x.IsCurrent);
//        }
//    }
//}
