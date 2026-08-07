using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> CreateAsync(Application application)
        {
            await _context.Set<Application>().AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<bool> ExistsAsync(int jobSeekerProfileId, int vacancyId)
        {
            return await _context.Set<Application>()
                .AnyAsync(a => a.JobSeekerProfileId == jobSeekerProfileId && a.VacancyId == vacancyId);
        }

        public async Task<Application?> GetByIdAsync(int applicationId)
        {
            return await _context.Set<Application>()
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        }

        public async Task<IEnumerable<Application>> GetByVacancyIdAsync(int vacancyId)
        {
            return await _context.Set<Application>()
                .Where(a => a.VacancyId == vacancyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetByJobSeekerProfileIdAsync(int jobSeekerProfileId)
        {
            return await _context.Set<Application>()
                .Where(a => a.JobSeekerProfileId == jobSeekerProfileId)
                .ToListAsync();
        }

        public async Task<bool> UpdateStatusAsync(int applicationId, ApplicationStatus status)
        {
            var application = await GetByIdAsync(applicationId);
            if (application == null) return false;

            application.Status = status;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        //Task IApplicationRepository.AddAsync(Application application) => throw new NotImplementedException();

        
    }
}