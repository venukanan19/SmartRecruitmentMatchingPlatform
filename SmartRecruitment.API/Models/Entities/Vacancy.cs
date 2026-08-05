namespace SmartRecruitment.API.Models.Entities
{
    public class Vacancy
    {
        public int VacancyId { get; set; }

        public int EmployerProfileId { get; set; }
        public EmployerProfile EmployerProfile { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int RequiredExperienceYears { get; set; }

        public string EducationRequirement { get; set; } = string.Empty;

        public bool IsClosed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public ICollection<VacancySkill> VacancySkills { get; set; }
        = new List<VacancySkill>();
    }
}