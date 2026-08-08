using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VacancyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vacancy?> GetByIdAsync(int vacancyId)
        {
            return await _dbContext.Vacancies
                .FirstOrDefaultAsync(
                    vacancy => vacancy.VacancyId == vacancyId);
        }

        public async Task<Vacancy?> GetByIdWithDetailsAsync(
            int vacancyId)
        {
            return await _dbContext.Vacancies
                .Include(vacancy => vacancy.EmployerProfile)
                .Include(vacancy => vacancy.VacancySkills)
                    .ThenInclude(vacancySkill => vacancySkill.Skill)
                .FirstOrDefaultAsync(
                    vacancy => vacancy.VacancyId == vacancyId);
        }

        public async Task<IReadOnlyList<Vacancy>> GetByEmployerIdAsync(
            int employerProfileId)
        {
            return await _dbContext.Vacancies
                .Include(vacancy => vacancy.EmployerProfile)
                .Include(vacancy => vacancy.VacancySkills)
                    .ThenInclude(vacancySkill => vacancySkill.Skill)
                .Where(
                    vacancy =>
                        vacancy.EmployerProfileId == employerProfileId)
                .OrderByDescending(
                    vacancy => vacancy.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Skill>> GetSkillsByIdsAsync(
            IEnumerable<int> skillIds)
        {
            var ids = skillIds
                .Distinct()
                .ToList();

            return await _dbContext.Skills
                .Where(skill => ids.Contains(skill.SkillId))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Vacancy>> SearchAsync(
            string? searchTerm,
            string? location,
            int? maxRequiredExperienceYears,
            int? skillId)
        {
            var query = _dbContext.Vacancies
                .Include(vacancy => vacancy.EmployerProfile)
                .Include(vacancy => vacancy.VacancySkills)
                    .ThenInclude(vacancySkill => vacancySkill.Skill)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim();

                query = query.Where(vacancy =>
                    vacancy.Title.Contains(term) ||
                    vacancy.Description.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                string locationValue = location.Trim();

                query = query.Where(vacancy =>
                    vacancy.Location.Contains(locationValue));
            }

            if (maxRequiredExperienceYears.HasValue)
            {
                query = query.Where(vacancy =>
                    vacancy.RequiredExperienceYears <=
                    maxRequiredExperienceYears.Value);
            }

            if (skillId.HasValue)
            {
                query = query.Where(vacancy =>
                    vacancy.VacancySkills.Any(vacancySkill =>
                        vacancySkill.SkillId == skillId.Value));
            }

            return await query
                .OrderByDescending(vacancy => vacancy.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Vacancy vacancy)
        {
            await _dbContext.Vacancies.AddAsync(vacancy);
        }

        public void Update(Vacancy vacancy)
        {
            _dbContext.Vacancies.Update(vacancy);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}