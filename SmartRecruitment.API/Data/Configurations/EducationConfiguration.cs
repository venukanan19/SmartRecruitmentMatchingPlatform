using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class EducationConfiguration
    {
        public int EducationId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string InstitutionName { get; set; }
            = string.Empty;

        public string Qualification { get; set; }
            = string.Empty;

        public string FieldOfStudy { get; set; }
            = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public JobSeekerProfile JobSeekerProfile
        { get; set; } = null!;
    }
}
