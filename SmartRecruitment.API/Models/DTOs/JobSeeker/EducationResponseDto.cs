namespace SmartRecruitment.API.Models.DTOs.JobSeeker
{
    public class EducationResponseDto
    {
        public int EducationId { get; set; }

        public string InstitutionName { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        public string FieldOfStudy { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
