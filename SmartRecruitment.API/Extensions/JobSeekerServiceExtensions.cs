using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using SmartRecruitment.API.Helpers;
using SmartRecruitment.API.Mappings;

using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Repositories.Interfaces;

using SmartRecruitment.API.Services;
using SmartRecruitment.API.Services.Interfaces;

using SmartRecruitment.API.Validators.JobSeeker;

namespace SmartRecruitment.API.Extensions
{
    public static class JobSeekerServiceExtensions
    {
        public static IServiceCollection AddJobSeekerModule(
            this IServiceCollection services)
        {
            services.AddScoped<
                IJobSeekerRepository,
                JobSeekerRepository>();

            services.AddScoped<
                ICvMetadataRepository,
                CvMetadataRepository>();
                
            services.AddScoped<
                IJobSeekerService,
                JobSeekerService>();

            services.AddScoped<
                ICvStorageService,
                CvStorageService>();

            services.AddScoped<FileValidationHelper>();
            services.AddScoped<SafeFileNameGenerator>();

            services.AddAutoMapper(
                typeof(JobSeekerMappingProfile).Assembly);

            services.AddValidatorsFromAssemblyContaining<
                UpdateProfileValidator>();

            return services;
        }
    }
}