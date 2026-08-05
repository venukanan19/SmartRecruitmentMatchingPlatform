using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly IVacancyRepository _vacancyRepository;

        public MatchingService(IVacancyRepository vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        public async Task<MatchResultDto> GetMatchScoreAsync(
            int vacancyId,
            int jobSeekerId)
        {
            // Step 1: Read Vacancy
            var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);


            if (vacancy is null)
            {
                throw new KeyNotFoundException($"Vacancy with ID {vacancyId} not found.");
            }

            // TODO:
            // Read JobSeekerProfile
            // Read JobSeekerSkills
            // Read VacancySkills

            // TODO:
            // Compare Skills

            // TODO:
            // Compare Experience
            // vacancy.RequiredExperienceYears

            // TODO:
            // Compare Education
            // vacancy.EducationRequirement

            // TODO:
            // Compare Location
            // vacancy.Location

            // TODO:
            // Calculate Total Score

            // TODO:
            // Find Missing Skills

            throw new NotImplementedException();
        }

        public async Task<List<RankedCandidateDto>> GetRankedCandidatesAsync(
            int vacancyId)
        {
            // Step 1: Read Vacancy
            var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);

            if (vacancy is null)
            {
                throw new KeyNotFoundException($"Vacancy with ID {vacancyId} not found.");
            }

            // TODO:
            // Get all applicants

            // TODO:
            // Calculate Match Score for each applicant

            // TODO:
            // Sort by TotalScore (Highest First)

            // TODO:
            // Assign Rank

            throw new NotImplementedException();
        }
    }
}