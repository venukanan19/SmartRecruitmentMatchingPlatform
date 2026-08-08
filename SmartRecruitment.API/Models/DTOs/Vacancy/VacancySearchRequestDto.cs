namespace SmartRecruitment.API.Models.DTOs.Vacancy
{
    public class VacancySearchRequestDto
    {
        public string? SearchTerm { get; set; }

        public string? Location { get; set; }

        public int? MaxRequiredExperienceYears { get; set; }

        public int? SkillId { get; set; }
    }
}