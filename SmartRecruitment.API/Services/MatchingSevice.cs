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
            // -------------------------------------------------
            // 1. Get vacancy with required skills
            // -------------------------------------------------

            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(
                    vacancyId);

            if (vacancy == null)
            {
                throw new KeyNotFoundException(
                    $"Vacancy with ID {vacancyId} was not found.");
            }

            // -------------------------------------------------
            // 2. Get Job Seeker profile
            // -------------------------------------------------

            var jobSeeker =
                await _jobSeekerRepository.GetByUserIdAsync(
                    jobSeekerProfileId);

            if (jobSeeker == null)
            {
                throw new KeyNotFoundException(
                    $"Job seeker with ID {jobSeekerProfileId} was not found.");
            }

            // -------------------------------------------------
            // 3. Get complete profile
            // -------------------------------------------------

            var profile =
    await _jobSeekerRepository
        .GetCompleteProfileByIdAsync(
            jobSeekerProfileId);

            if (profile == null)
            {
                throw new KeyNotFoundException(
                    $"Job seeker profile with ID {jobSeekerProfileId} was not found.");
            }

            // -------------------------------------------------
            // 4. Skill matching
            // -------------------------------------------------

            var requiredSkills = vacancy.VacancySkills
                .Select(vs => vs.Skill.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim().ToLower())
                .Distinct()
                .ToList();

            var candidateSkills = profile.JobSeekerSkills
                .Select(js => js.Skill.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim().ToLower())
                .Distinct()
                .ToList();

            var matchedSkills = requiredSkills
                .Intersect(candidateSkills)
                .ToList();

            var missingSkills = requiredSkills
                .Except(candidateSkills)
                .ToList();

            double skillScore = 0;

            if (requiredSkills.Count > 0)
            {
                skillScore =
                    ((double)matchedSkills.Count /
                    requiredSkills.Count) *
                    SkillWeight;
            }

            // -------------------------------------------------
            // 5. Experience matching
            // -------------------------------------------------

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

            double experienceScore = 0;

            if (vacancy.RequiredExperienceYears <= 0)
            {
                experienceScore = ExperienceWeight;
            }
            else
            {
                double experienceRatio =
                    totalExperienceYears /
                    vacancy.RequiredExperienceYears;

                experienceRatio =
                    Math.Min(experienceRatio, 1.0);

                experienceScore =
                    experienceRatio *
                    ExperienceWeight;
            }

            // -------------------------------------------------
            // 6. Education matching
            // -------------------------------------------------

            double educationScore = 0;

            string requiredEducation =
                vacancy.EducationRequirement?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(requiredEducation))
            {
                educationScore = EducationWeight;
            }
            else
            {
                bool educationMatched =
                    profile.Educations.Any(education =>
                        education.Qualification
                            .Contains(
                                requiredEducation,
                                StringComparison.OrdinalIgnoreCase)
                        ||
                        education.FieldOfStudy
                            .Contains(
                                requiredEducation,
                                StringComparison.OrdinalIgnoreCase));

                if (educationMatched)
                {
                    educationScore = EducationWeight;
                }
            }

            // -------------------------------------------------
            // 7. Total score
            // -------------------------------------------------

            double totalScore =
                skillScore +
                experienceScore +
                educationScore;

            // -------------------------------------------------
            // 8. Return result
            // -------------------------------------------------

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
                await _vacancyRepository.GetByIdWithDetailsAsync(
                    vacancyId);

            if (vacancy == null)
            {
                throw new KeyNotFoundException(
                    $"Vacancy with ID {vacancyId} was not found.");
            }

            // This method should obtain the applicants for this vacancy
            // from the Application repository.
            //
            // Therefore, ranking cannot be completed correctly with
            // only IVacancyRepository + IJobSeekerRepository.

            return new List<RankedCandidateDto>();
        }
    }
}