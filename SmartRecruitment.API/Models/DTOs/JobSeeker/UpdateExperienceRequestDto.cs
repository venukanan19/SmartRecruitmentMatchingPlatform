namespace SmartRecruitment.API.Models.DTOs.JobSeeker
{
    public class UpdateExperienceRequestDto
    {
        public string CompanyName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrentJob { get; set; }
    }
}
