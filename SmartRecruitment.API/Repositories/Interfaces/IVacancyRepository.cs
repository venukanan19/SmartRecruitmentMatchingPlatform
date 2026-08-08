using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IVacancyRepository
    {
        // Get a vacancy by ID
        Task<Vacancy?> GetByIdAsync(int vacancyId);

        // Get vacancy together with employer and required skills
        // Used by the Matching Engine
        Task<Vacancy?> GetByIdWithDetailsAsync(int vacancyId);

        // Get vacancies belonging to an employer
        Task<IReadOnlyList<Vacancy>> GetByEmployerIdAsync(
            int employerProfileId);

        // Get skills by their IDs
        Task<IReadOnlyList<Skill>> GetSkillsByIdsAsync(
            IEnumerable<int> skillIds);

        // Search and filter vacancies
        Task<IReadOnlyList<Vacancy>> SearchAsync(
            string? searchTerm,
            string? location,
            int? maxRequiredExperienceYears,
            int? skillId);

        // Add a new vacancy
        Task AddAsync(Vacancy vacancy);

        // Update an existing vacancy
        void Update(Vacancy vacancy);

        // Save database changes
        Task<bool> SaveChangesAsync();
    }
}