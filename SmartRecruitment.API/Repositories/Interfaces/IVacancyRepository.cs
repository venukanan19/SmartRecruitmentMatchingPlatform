using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetByIdAsync(int vacancyId);

        Task<Vacancy?> GetByIdWithDetailsAsync(int vacancyId);

        Task<IReadOnlyList<Vacancy>> GetByEmployerIdAsync(
            int employerProfileId);

        Task<IReadOnlyList<Skill>> GetSkillsByIdsAsync(
            IEnumerable<int> skillIds);

        // Vacancy search and filter
        Task<IReadOnlyList<Vacancy>> SearchAsync(
            string? searchTerm,
            string? location,
            int? maxRequiredExperienceYears,
            int? skillId);

        Task AddAsync(Vacancy vacancy);

        void Update(Vacancy vacancy);

        Task<bool> SaveChangesAsync();
    }
}