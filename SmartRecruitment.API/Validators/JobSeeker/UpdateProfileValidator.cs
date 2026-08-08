//using SmartRecruitment.API.Models.DTOs.JobSeeker;

//namespace SmartRecruitment.API.Validators.JobSeeker
//{
//    public class UpdateProfileValidator : AbstractValidator<UpdateJobSeekerProfileRequestDto>
//    {
//        public UpdateProfileValidator()
//        {
//            RuleFor(x => x.FirstName)
//                .NotEmpty()
//                .MaximumLength(100);

//            RuleFor(x => x.LastName)
//                .NotEmpty()
//                .MaximumLength(100);

//            RuleFor(x => x.PhoneNumber)
//                .MaximumLength(30);

//            RuleFor(x => x.Address)
//                .MaximumLength(250);

//            RuleFor(x => x.City)
//                .NotEmpty()
//                .MaximumLength(100);

//            RuleFor(x => x.Country)
//                .NotEmpty()
//                .MaximumLength(100);

//            RuleFor(x => x.ProfessionalSummary)
//                .MaximumLength(2000);

//            RuleFor(x => x.DateOfBirth)
//                .LessThan(DateTime.UtcNow.Date)
//                .When(x => x.DateOfBirth.HasValue);
//        }
//    }
//}
