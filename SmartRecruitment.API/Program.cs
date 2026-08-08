using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Data.Seed;
using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services;
using SmartRecruitment.API.Services.Interfaces;
using SmartRecruitment.API.Validators.Employer;

namespace SmartRecruitment.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers
            builder.Services.AddControllers();

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            // FluentValidation
            // Registers all validators in this assembly
            builder.Services.AddValidatorsFromAssemblyContaining<
                CreateEmployerProfileValidator>();

            // Member 3 Repository registrations
            builder.Services.AddScoped<
                IEmployerRepository,
                EmployerRepository>();

            builder.Services.AddScoped<
                IVacancyRepository,
                VacancyRepository>();

            // Member 3 Service registrations
            builder.Services.AddScoped<
                IEmployerService,
                EmployerService>();

            builder.Services.AddScoped<
                IVacancyService,
                VacancyService>();

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Seed approved master skills
            using (var scope = app.Services.CreateScope())
            {
                var dbContext =
                    scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                await SkillSeed.SeedAsync(dbContext);
            }

            // Development OpenAPI
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}