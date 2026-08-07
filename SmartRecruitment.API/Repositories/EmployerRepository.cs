using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployerProfile?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.EmployerProfiles
                .FirstOrDefaultAsync(
                    employer => employer.UserId == userId);
        }

        public async Task<EmployerProfile?> GetByIdAsync(
            int employerProfileId)
        {
            return await _dbContext.EmployerProfiles
                .FirstOrDefaultAsync(
                    employer =>
                        employer.EmployerProfileId == employerProfileId);
        }

        public async Task AddAsync(EmployerProfile employerProfile)
        {
            await _dbContext.EmployerProfiles.AddAsync(employerProfile);
        }

        public void Update(EmployerProfile employerProfile)
        {
            _dbContext.EmployerProfiles.Update(employerProfile);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}