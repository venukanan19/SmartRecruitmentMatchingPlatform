namespace SmartRecruitment.API.Models.DTOs.JobSeeker
{
    public class JobSeekerProfileResponseDto
    {
        public int JobSeekerProfileId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public IReadOnlyList<JobSeekerSkillResponseDto> Skills { get; set; }
            = new List<JobSeekerSkillResponseDto>();

        public IReadOnlyList<EducationResponseDto> Educations { get; set; }
            = new List<EducationResponseDto>();

        public IReadOnlyList<ExperienceResponseDto> Experiences { get; set; }
            = new List<ExperienceResponseDto>();

        public CvMetadataResponseDto? CvMetadata { get; set; }
    }
}
