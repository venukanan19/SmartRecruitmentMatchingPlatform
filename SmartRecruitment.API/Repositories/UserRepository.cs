using SmartRecruitment.API.Data;
using SmartRecruitment.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
        public class UserRepository : IUserRepository
        {
            private readonly ApplicationDbContext _dbContext;

            public UserRepository(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<User?> GetByIdAsync(
                int userId,
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Users
                    .FirstOrDefaultAsync(
                        user => user.UserId == userId,
                        cancellationToken);
            }

            public async Task<User?> GetByEmailAsync(
                string email,
                CancellationToken cancellationToken = default)
            {
                string normalizedEmail = NormalizeEmail(email);

                return await _dbContext.Users
                    .FirstOrDefaultAsync(
                        user => user.Email == normalizedEmail,
                        cancellationToken);
            }

            public async Task<bool> EmailExistsAsync(
                string email,
                CancellationToken cancellationToken = default)
            {
                string normalizedEmail = NormalizeEmail(email);

                return await _dbContext.Users
                    .AnyAsync(
                        user => user.Email == normalizedEmail,
                        cancellationToken);
            }

            public async Task<IReadOnlyList<User>> GetAllAsync(
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Users
                    .AsNoTracking()
                    .OrderByDescending(user => user.CreatedAt)
                    .ToListAsync(cancellationToken);
            }

            public async Task<int> CountAllAsync(
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Users
                    .CountAsync(cancellationToken);
            }

            public async Task<int> CountActiveAsync(
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Users
                    .CountAsync(
                        user => user.IsActive,
                        cancellationToken);
            }

            public async Task<int> CountByRoleAsync(
                string role,
                CancellationToken cancellationToken = default)
            {
                return await _dbContext.Users
                    .CountAsync(
                        user => user.Role == role,
                        cancellationToken);
            }

            public async Task AddAsync(
                User user,
                CancellationToken cancellationToken = default)
            {
                await _dbContext.Users.AddAsync(
                    user,
                    cancellationToken);
            }

            public Task UpdateAsync(
                User user,
                CancellationToken cancellationToken = default)
            {
                _dbContext.Users.Update(user);

                return Task.CompletedTask;
            }

            public async Task SaveChangesAsync(
                CancellationToken cancellationToken = default)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            private static string NormalizeEmail(string email)
            {
                return email.Trim().ToLowerInvariant();
            }
        }
    
}
