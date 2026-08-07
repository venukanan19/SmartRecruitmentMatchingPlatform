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
            // TODO:
            // Read Vacancy
            // Read JobSeekerProfile
            // Compare Skills
            // Compare Experience
            // Compare Education
            // Calculate final score

            return await Task.FromResult(new MatchResultDto
            {
                VacancyId = vacancyId,
                JobSeekerProfileId = jobSeekerProfileId,
                SkillScore = 40,
                ExperienceScore = 25,
                EducationScore = 20,
                TotalScore = 85,
                MissingSkills = new List<string>
        {
            "Docker",
            "Azure"
        }
            });
        }

        public async Task<IEnumerable<RankedCandidateDto>> GetRankedCandidatesAsync(int vacancyId)
        {
            var candidates = new List<RankedCandidateDto>
    {
        new RankedCandidateDto
        {
            Rank = 1,
            JobSeekerProfileId = 101,
            CandidateName = "Candidate A",
            TotalScore = 92
        },
        new RankedCandidateDto
        {
            Rank = 2,
            JobSeekerProfileId = 102,
            CandidateName = "Candidate B",
            TotalScore = 88
        },
        new RankedCandidateDto
        {
            Rank = 3,
            JobSeekerProfileId = 103,
            CandidateName = "Candidate C",
            TotalScore = 79
        }
    };

            return await Task.FromResult(candidates);
        }
    }
}