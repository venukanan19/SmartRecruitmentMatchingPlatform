using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class MatchingService : IMatchingService
    {
        // Approved Rule-Based Engine Weights (Total = 100%)
        private const double SKILL_WEIGHT = 50.0;
        private const double EXPERIENCE_WEIGHT = 30.0;
        private const double EDUCATION_WEIGHT = 20.0;

        public async Task<MatchResultDto> CalculateMatchAsync(int jobSeekerProfileId, int vacancyId)
        {
            // Placeholder evaluation logic — ready to integrate with SkillGap/Profile services
            double skillScore = 40.0;     // Out of 50
            double expScore = 25.0;       // Out of 30
            double eduScore = 20.0;       // Out of 20
            double total = skillScore + expScore + eduScore;

            var missingSkills = new List<string> { "Docker", "Azure" };

            return await Task.FromResult(new MatchResultDto
            {
                VacancyId = vacancyId,
                JobSeekerProfileId = jobSeekerProfileId,
                SkillScore = skillScore,
                ExperienceScore = expScore,
                EducationScore = eduScore,
                TotalScore = total,
                MissingSkills = missingSkills
            });
        }

        public async Task<IEnumerable<RankedCandidateDto>> GetRankedCandidatesAsync(int vacancyId)
        {
            // Dummy collection ranked in descending score order
            var candidates = new List<RankedCandidateDto>
            {
                new RankedCandidateDto { Rank = 1, JobSeekerProfileId = 101, CandidateName = "Candidate A", TotalScore = 92.5 },
                new RankedCandidateDto { Rank = 2, JobSeekerProfileId = 102, CandidateName = "Candidate B", TotalScore = 85.0 },
                new RankedCandidateDto { Rank = 3, JobSeekerProfileId = 103, CandidateName = "Candidate C", TotalScore = 78.0 }
            };

            return await Task.FromResult(candidates);
        }
    }
}