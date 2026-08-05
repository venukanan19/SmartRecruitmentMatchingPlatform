namespace SmartRecruitment.API.Models.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string InstitutionName { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        public string FieldOfStudy { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // New fields

        // True if the education is still ongoing
        public bool IsCurrent { get; set; }

        // Record creation time
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property

        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;
    }
}
