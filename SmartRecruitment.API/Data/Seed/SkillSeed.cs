using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Seed
{
    public static class SkillSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext dbContext,
            CancellationToken cancellationToken = default)
        {
            string[] skillNames =
            {
                "C#",
                "ASP.NET Core",
                "Java",
                "Python",
                "JavaScript",
                "TypeScript",
                "HTML",
                "CSS",
                "Bootstrap",
                "React",
                "Angular",
                "Node.js",
                "SQL Server",
                "MySQL",
                "PostgreSQL",
                "MongoDB",
                "Git",
                "GitHub",
                "REST API",
                "Entity Framework Core",
                "LINQ",
                "JWT Authentication",
                "Docker",
                "Azure",
                "AWS",
                "Unit Testing",
                "Integration Testing",
                "Postman",
                "Swagger",
                "OOP",
                "Problem Solving",
                "Communication",
                "Teamwork"
            };

            var existingSkillNames = await dbContext.Skills
                .Select(skill => skill.Name)
                .ToListAsync(cancellationToken);

            var newSkills = skillNames
                .Where(skillName =>
                    !existingSkillNames.Contains(skillName))
                .Select(skillName => new Skill
                {
                    Name = skillName
                })
                .ToList();

            if (newSkills.Count == 0)
            {
                return;
            }

            await dbContext.Skills.AddRangeAsync(
                newSkills,
                cancellationToken);

            await dbContext.SaveChangesAsync(
                cancellationToken);
        }
    }
}