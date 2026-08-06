namespace SmartRecruitment.API.Models.Entities
{
    public class JobSeekerProfile
    {

        public int JobSeekerProfileId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        public User User { get; set; } = null!;

        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
            = new List<JobSeekerSkill>();

        public ICollection<Education> Educations { get; set; }
            = new List<Education>();

        public ICollection<Experience> Experiences { get; set; }
            = new List<Experience>();

        public CvMetadata? CvMetadata { get; set; }


    }
}