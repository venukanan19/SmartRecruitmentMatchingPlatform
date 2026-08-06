namespace SmartRecruitment.API.Models.Entities
{
    public class JobSeekerSkill
    {
        public int JobSeekerProfileId { get; set; }

        public int SkillId { get; set; }

        // Navigation properties

        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;

        public Skill Skill { get; set; } = null!;
    }
}
