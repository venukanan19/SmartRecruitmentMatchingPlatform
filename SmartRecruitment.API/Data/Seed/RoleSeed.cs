using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Helpers;
using SmartRecruitment.API.Models.Entities;
namespace SmartRecruitment.API.Data.Seed
{
    public static class RoleSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            CancellationToken cancellationToken = default)
        {
            string? adminEmail =
                configuration["SeedAdmin:Email"];

            string? adminPassword =
                configuration["SeedAdmin:Password"];

            string adminFullName =
                configuration["SeedAdmin:FullName"]
                ?? "System Administrator";

            if (string.IsNullOrWhiteSpace(adminEmail) ||
                string.IsNullOrWhiteSpace(adminPassword))
            {
                return;
            }

            string normalizedEmail =
                adminEmail.Trim().ToLowerInvariant();

            bool adminExists =
                await dbContext.Users.AnyAsync(
                    user => user.Email == normalizedEmail,
                    cancellationToken);

            if (adminExists)
            {
                return;
            }

            User administrator = new()
            {
                FullName = adminFullName.Trim(),
                Email = normalizedEmail,
                PasswordHash =
                    PasswordHashHelper.HashPassword(
                        adminPassword),
                Role = UserRole.Admin.ToString(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await dbContext.Users.AddAsync(
                administrator,
                cancellationToken);

            await dbContext.SaveChangesAsync(
                cancellationToken);
        }
    }
}
