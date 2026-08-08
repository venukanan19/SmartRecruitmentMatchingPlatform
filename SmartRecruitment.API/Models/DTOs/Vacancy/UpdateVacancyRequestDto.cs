namespace SmartRecruitment.API.Models.DTOs.Vacancy
{
    public class UpdateVacancyRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int RequiredExperienceYears { get; set; }

        public string EducationRequirement { get; set; } = string.Empty;

        public List<int> RequiredSkillIds { get; set; } = new();
    }
}