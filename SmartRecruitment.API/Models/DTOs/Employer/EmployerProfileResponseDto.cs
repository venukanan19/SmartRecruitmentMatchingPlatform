namespace SmartRecruitment.API.Models.DTOs.Employer
{
    public class EmployerProfileResponseDto
    {
        public int EmployerProfileId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyDescription { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string? Website { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}