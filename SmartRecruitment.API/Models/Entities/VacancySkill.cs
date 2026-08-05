namespace SmartRecruitment.API.Models.Entities
{
    public class VacancySkill
    {
        public int VacancyId { get; set; }

        public Vacancy Vacancy { get; set; } = null!;

        public int SkillId { get; set; }

        public Skill Skill { get; set; } = null!;
    }
}