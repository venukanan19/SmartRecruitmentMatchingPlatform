namespace SmartRecruitment.API.Models.Entities
{
    public class JobSeekerSkill
    {
        public int JobSeekerProfileId { get; set; }

        public int SkillId { get; set; }

        // New fields

        // Value between 1 and 5
        public int ProficiencyLevel { get; set; }

        // Number of years of experience
        public int YearsOfExperience { get; set; }

        // Navigation properties

        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;

      //  public Skill Skill { get; set; } = null!;
    }
}
