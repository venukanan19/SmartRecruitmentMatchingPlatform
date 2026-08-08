namespace SmartRecruitment.API.Models.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        public string Name { get; set; } = string.Empty;

        // Employer side
        public ICollection<VacancySkill> VacancySkills { get; set; }
            = new List<VacancySkill>();

        // Job Seeker side
        public ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
            = new List<JobSeekerSkill>();
    }
}