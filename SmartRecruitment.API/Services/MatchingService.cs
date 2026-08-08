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
        private readonly RankingService _rankingService;
        private readonly SkillGapService _skillGapService;
        private readonly IApplicationRepository _applicationRepository;

        // Approved BRD Weights
        private const double SkillWeight = 50.0;
        private const double ExperienceWeight = 30.0;
        private const double EducationWeight = 20.0;

        public MatchingService(
              IVacancyRepository vacancyRepository,
              IJobSeekerRepository jobSeekerRepository,
              IApplicationRepository applicationRepository,
              RankingService rankingService,
              SkillGapService skillGapService)
        {
            _vacancyRepository = vacancyRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _applicationRepository = applicationRepository;
            _rankingService = rankingService;
            _skillGapService = skillGapService;
        }

        public async Task<MatchResultDto> CalculateMatchAsync(
           int userId,
           int vacancyId)
        {
            // ==========================================================
            // Vacancy
            // ==========================================================

            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(
                    vacancyId);

            if (vacancy == null)
            {
                throw new KeyNotFoundException(
                    $"Vacancy with ID {vacancyId} was not found.");
            }

            // ==========================================================
            // Job Seeker
            // ==========================================================

            var profile =
                await _jobSeekerRepository
              .GetCompleteProfileByUserIdAsync(
            userId);

            if (profile == null)
            {
                throw new KeyNotFoundException(
                    $"Job Seeker for User ID {userId} was not found.");
            }

            // ==========================================================
            // Required Skills
            // ==========================================================

            var requiredSkills =
                vacancy.VacancySkills
                    .Select(vs => vs.Skill.Name)
                    .Where(name =>
                        !string.IsNullOrWhiteSpace(name))
                    .Select(name =>
                        name.Trim().ToLower())
                    .Distinct()
                    .ToList();

            var candidateSkills =
                profile.JobSeekerSkills
                    .Select(js => js.Skill.Name)
                    .Where(name =>
                        !string.IsNullOrWhiteSpace(name))
                    .Select(name =>
                        name.Trim().ToLower())
                    .Distinct()
                    .ToList();

            var matchedSkills =
            _skillGapService.GetMatchedSkills(
              profile,
               vacancy);

            var missingSkills =
                _skillGapService.GetMissingSkills(
                    profile,
                    vacancy);

            // ==========================================================
            // Skill Score
            // ==========================================================

            double skillScore = 0;

            if (requiredSkills.Count > 0)
            {
                skillScore =
                    ((double)matchedSkills.Count /
                     requiredSkills.Count)
                    * SkillWeight;
            }

            // ==========================================================
            // Experience Score
            // ==========================================================

            double totalExperienceYears = 0;

            foreach (var experience in profile.Experiences)
            {
                DateTime endDate =
                    experience.EndDate ??
                    DateTime.UtcNow;

                if (endDate > experience.StartDate)
                {
                    totalExperienceYears +=
                        (endDate - experience.StartDate)
                        .TotalDays / 365.25;
                }
            }

            double experienceScore = 0;

            if (vacancy.RequiredExperienceYears <= 0)
            {
                experienceScore = ExperienceWeight;
            }
            else
            {
                double ratio =
                    totalExperienceYears /
                    vacancy.RequiredExperienceYears;

                ratio = Math.Min(ratio, 1);

                experienceScore =
                    ratio *
                    ExperienceWeight;
            }

            // ==========================================================
            // Education Score
            // ==========================================================

            double educationScore = 0;

            string requiredEducation =
                vacancy.EducationRequirement?.Trim()
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(requiredEducation))
            {
                educationScore =
                    EducationWeight;
            }
            else
            {
                bool matched =
                    profile.Educations.Any(
                        education =>
                            education.Qualification.Contains(
                                requiredEducation,
                                StringComparison.OrdinalIgnoreCase)
                            ||
                            education.FieldOfStudy.Contains(
                                requiredEducation,
                                StringComparison.OrdinalIgnoreCase));

                if (matched)
                {
                    educationScore =
                        EducationWeight;
                }
            }

            // ==========================================================
            // Final Score
            // ==========================================================

            double totalScore =
                skillScore +
                experienceScore +
                educationScore;

            return new MatchResultDto
            {
                JobSeekerProfileId =
                 profile.JobSeekerProfileId,

                VacancyId =
                    vacancyId,

                SkillScore =
                    Math.Round(skillScore, 2),

                ExperienceScore =
                    Math.Round(experienceScore, 2),

                EducationScore =
                    Math.Round(educationScore, 2),

                TotalScore =
                    Math.Round(totalScore, 2),

                MissingSkills =
                    missingSkills
            };
        }

        public async Task<IEnumerable<RankedCandidateDto>>
            GetRankedCandidatesAsync(
                int vacancyId)
        {
            var applications =
    await _applicationRepository
        .GetApplicationsByVacancyIdAsync(
            vacancyId);

            var candidates =
                new List<RankedCandidateDto>();

            foreach (var application in applications)
            {
                var result =
                    await CalculateMatchAsync(
                        application.JobSeekerProfileId,
                        vacancyId);

                candidates.Add(
                    new RankedCandidateDto
                    {
                        JobSeekerProfileId =
                            application.JobSeekerProfileId,

                        CandidateName =
                            application.JobSeekerProfile.FirstName + " " +
                            application.JobSeekerProfile.LastName,

                        TotalScore =
                            result.TotalScore
                    });
            }

            return _rankingService
                .GetRankedCandidates(candidates);
        }

    }
}