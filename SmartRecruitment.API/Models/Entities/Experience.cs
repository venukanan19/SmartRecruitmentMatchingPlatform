namespace SmartRecruitment.API.Models.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrentJob { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation Property

        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;
    }

}

