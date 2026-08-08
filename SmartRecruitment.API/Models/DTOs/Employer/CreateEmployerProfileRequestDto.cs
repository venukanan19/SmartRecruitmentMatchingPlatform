namespace SmartRecruitment.API.Models.DTOs.Employer
{
    public class CreateEmployerProfileRequestDto
    {
        public string CompanyName { get; set; } = string.Empty;

        public string CompanyDescription { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string? Website { get; set; }
    }
}