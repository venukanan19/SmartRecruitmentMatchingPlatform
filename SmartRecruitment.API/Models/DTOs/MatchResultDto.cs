namespace SmartRecruitment.API.Models.DTOs
{
    public class MatchResultDto
    {
        internal int JobSeekerProfileId;

        public int VacancyId { get; set; }

        public int JobSeekerId { get; set; }

        public double SkillScore { get; set; }

        public double ExperienceScore { get; set; }

        public double EducationScore { get; set; }

        public double TotalScore { get; set; }

        public List<string> MissingSkills { get; set; } = new();
    }
}