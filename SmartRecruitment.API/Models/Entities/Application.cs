using SmartRecruitment.API.Enums;

namespace SmartRecruitment.API.Models.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public int VacancyId { get; set; }

        public string? CoverLetter { get; set; }

        public ApplicationStatus Status { get; set; }
            = ApplicationStatus.Applied;

        public DateTime AppliedDate { get; set; }
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public JobSeekerProfile JobSeekerProfile { get; set; }
            = null!;

        public Vacancy Vacancy { get; set; }
            = null!;
    }
}