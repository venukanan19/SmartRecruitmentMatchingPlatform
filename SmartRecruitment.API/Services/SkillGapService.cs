using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Services
{
    public class SkillGapService
    {
        public List<string> GetMissingSkills(
            JobSeekerProfile profile,
            Vacancy vacancy)
        {
            var candidateSkills = profile.JobSeekerSkills
                .Select(skill => skill.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            var requiredSkills = vacancy.VacancySkills
                .Select(skill => skill.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            return requiredSkills
                .Except(candidateSkills)
                .ToList();
        }

        public List<string> GetMatchedSkills(
            JobSeekerProfile profile,
            Vacancy vacancy)
        {
            var candidateSkills = profile.JobSeekerSkills
                .Select(skill => skill.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            var requiredSkills = vacancy.VacancySkills
                .Select(skill => skill.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            return requiredSkills
                .Intersect(candidateSkills)
                .ToList();
        }
    }
}