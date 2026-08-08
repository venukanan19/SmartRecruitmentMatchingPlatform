namespace SmartRecruitment.API.Models.DTOs.Vacancy
{
    public class VacancyResponseDto
    {
        public int VacancyId { get; set; }

        public int EmployerProfileId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int RequiredExperienceYears { get; set; }

        public string EducationRequirement { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public List<string> RequiredSkills { get; set; } = new();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}