using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
    public class ApplicationRepository
        : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> CreateAsync(
            Application application)
        {
            await _context.Applications.AddAsync(application);

            await _context.SaveChangesAsync();

            return application;
        }

        public async Task<bool> ExistsAsync(
            int jobSeekerProfileId,
            int vacancyId)
        {
            return await _context.Applications.AnyAsync(
                application =>
                    application.JobSeekerProfileId ==
                    jobSeekerProfileId
                    &&
                    application.VacancyId ==
                    vacancyId);
        }

        public async Task<Application?> GetByIdAsync(
            int applicationId)
        {
            return await _context.Applications
                .FirstOrDefaultAsync(
                    application =>
                        application.ApplicationId ==
                        applicationId);
        }

        public async Task<IEnumerable<Application>>
            GetByVacancyIdAsync(int vacancyId)
        {
            return await _context.Applications
                .Where(
                    application =>
                        application.VacancyId ==
                        vacancyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>>
            GetByJobSeekerProfileIdAsync(
                int jobSeekerProfileId)
        {
            return await _context.Applications
                .Where(
                    application =>
                        application.JobSeekerProfileId ==
                        jobSeekerProfileId)
                .ToListAsync();
        }

        public async Task<bool> UpdateStatusAsync(
            int applicationId,
            ApplicationStatus status)
        {
            var application =
                await GetByIdAsync(applicationId);

            if (application == null)
            {
                return false;
            }

            application.Status = status;
            application.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}