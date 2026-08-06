
namespace SmartRecruitment.API.Models
{// 1. Define the enum here
    public enum ApplicationStatus
    {
        Applied = 0,
        UnderReview = 1,
        Shortlisted = 2,
        Rejected = 3,
        Accepted = 4
    }
    public class Application
    {
        //Primary Key
        public int ApplicationId { get; set; }

        // Foreign Keys
        public int JobSeekerProfileId { get; set; }
        public int VacancyId { get; set; }

        // Additional Fields
        public string? CoverLetter { get; set; }
        public ApplicationStatus Status{ get; set; } = ApplicationStatus.Applied;
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties (Uncomment once JobSeekerProfile and Vacancy entities are pushed by Member 2 & Member 3)
        // public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;
        // public virtual Vacancy Vacancy { get; set; } = null!;
    }
}