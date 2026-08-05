namespace SmartRecruitment.API.Models.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<VacancySkill> VacancySkills { get; set; }
            = new List<VacancySkill>();
    }
}