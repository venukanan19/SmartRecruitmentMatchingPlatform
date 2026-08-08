using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;

        // Approved matching weights
        private const double SkillWeight = 50.0;
        private const double ExperienceWeight = 30.0;
        private const double EducationWeight = 20.0;

        public MatchingService(
            IVacancyRepository vacancyRepository,
            IJobSeekerRepository jobSeekerRepository)
        {
            _vacancyRepository = vacancyRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }

        public async Task<MatchResultDto> CalculateMatchAsync(
            int jobSeekerProfileId,
            int vacancyId)
        {
            // -----------------------------------------
            // Get Vacancy
            // -----------------------------------------

            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(vacancyId);

            if (vacancy == null)
            {
                throw new KeyNotFoundException(
                    $"Vacancy with ID {vacancyId} was not found.");
            }

            // -----------------------------------------
            // Get Complete Job Seeker Profile
            // -----------------------------------------

            var profile =
                await _jobSeekerRepository
                    .GetCompleteProfileByUserIdAsync(jobSeekerProfileId);

            if (profile == null)
            {
                throw new KeyNotFoundException(
                    $"Job seeker profile with ID {jobSeekerProfileId} was not found.");
            }

            // -----------------------------------------
            // Skill Matching
            // -----------------------------------------

            var requiredSkills = vacancy.VacancySkills
                .Select(vs => vs.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            var candidateSkills = profile.JobSeekerSkills
                .Select(js => js.Skill.Name.Trim().ToLower())
                .Distinct()
                .ToList();

            var matchedSkills = requiredSkills
                .Intersect(candidateSkills)
                .ToList();

            var missingSkills = requiredSkills
                .Except(candidateSkills)
                .ToList();

            double skillScore = 0;

            if (requiredSkills.Any())
            {
                skillScore =
                    ((double)matchedSkills.Count /
                    requiredSkills.Count)
                    * SkillWeight;
            }

            // -----------------------------------------
            // Experience Matching
            // -----------------------------------------

            double totalExperienceYears = 0;

            foreach (var experience in profile.Experiences)
            {
                DateTime endDate =
                    experience.EndDate ?? DateTime.UtcNow;

                if (endDate > experience.StartDate)
                {
                    totalExperienceYears +=
                        (endDate - experience.StartDate).TotalDays / 365.25;
                }
            }

            double experienceScore;

            if (vacancy.RequiredExperienceYears <= 0)
            {
                experienceScore = ExperienceWeight;
            }
            else
            {
                double ratio =
                    Math.Min(
                        totalExperienceYears /
                        vacancy.RequiredExperienceYears,
                        1);

                experienceScore =
                    ratio * ExperienceWeight;
            }

            // -----------------------------------------
            // Education Matching
            // -----------------------------------------

            double educationScore = 0;

            string requiredEducation =
                vacancy.EducationRequirement?.Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(requiredEducation))
            {
                educationScore = EducationWeight;
            }
            else
            {
                bool matched =
                    profile.Educations.Any(e =>
                        e.Qualification.Contains(
                            requiredEducation,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        e.FieldOfStudy.Contains(
                            requiredEducation,
                            StringComparison.OrdinalIgnoreCase));

                if (matched)
                {
                    educationScore = EducationWeight;
                }
            }

            // -----------------------------------------
            // Final Score
            // -----------------------------------------

            double totalScore =
                skillScore +
                experienceScore +
                educationScore;

            return new MatchResultDto
            {
                JobSeekerProfileId = jobSeekerProfileId,
                VacancyId = vacancyId,
                SkillScore = Math.Round(skillScore, 2),
                ExperienceScore = Math.Round(experienceScore, 2),
                EducationScore = Math.Round(educationScore, 2),
                TotalScore = Math.Round(totalScore, 2),
                MissingSkills = missingSkills
            };
        }

        public async Task<IEnumerable<RankedCandidateDto>>
            GetRankedCandidatesAsync(int vacancyId)
        {
            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(vacancyId);

            if (vacancy == null)
            {
                throw new KeyNotFoundException(
                    $"Vacancy with ID {vacancyId} was not found.");
            }

            // Ranking logic will be implemented
            // after ApplicationRepository integration.

            return new List<RankedCandidateDto>();
        }
    }
}