 
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Data.Seed;
using SmartRecruitment.API.Extensions;
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

            // Member 1 - Authentication and Administration
            builder.Services.AddAuthenticationModule(
               builder.Configuration);


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

            // Member 2 - Job Seeker
            builder.Services.AddJobSeekerModule();
            
            // Member 4 - Matching & Applications
            

            builder.Services.AddScoped<
                IApplicationRepository,
                ApplicationRepository>();

            builder.Services.AddScoped<
                IApplicationService,
                ApplicationService>();

            builder.Services.AddScoped<
                IMatchingService,
                MatchingService>();

            builder.Services.AddScoped<
                RankingService>();

            builder.Services.AddScoped<
                SkillGapService>();

           
            // Member 5 - Contact Requests
          

            builder.Services.AddScoped<
                IContactRequestRepository,
                ContactRequestRepository>();

            builder.Services.AddScoped<
                IContactRequestService,
                ContactRequestService>();

            builder.Services.AddScoped<
                INotificationService,
                NotificationService>();

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Member 1 - Seed initial administrator
            using (var scope = app.Services.CreateScope())
            {
                var dbContext =
                    scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                await RoleSeed.SeedAsync(
                    dbContext,
                    builder.Configuration);
            }

            // Development OpenAPI + Swagger UI
             
            if (app.Environment.IsDevelopment())
            {
                // Generates:
                // /openapi/v1.json
                app.MapOpenApi();

                // Swagger UI
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/openapi/v1.json",
                        "Smart Recruitment API v1");

                    options.RoutePrefix = "swagger";
                });
            }

 
            // Middleware
    
            app.UseHttpsRedirection();

            // IMPORTANT: Authentication must come first
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}