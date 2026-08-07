using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Member 1 / Shared
        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        
        public DbSet<ContactRequest> ContactRequests { get; set; }

        // Member 2 DbSets
        public DbSet<JobSeekerProfile> JobSeekerProfiles
            => Set<JobSeekerProfile>();

        public DbSet<JobSeekerSkill> JobSeekerSkills
            => Set<JobSeekerSkill>();

        public DbSet<Education> Educations
            => Set<Education>();

        public DbSet<Experience> Experiences
            => Set<Experience>();

        public DbSet<CvMetadata> CvMetadata
            => Set<CvMetadata>();

        // Member 3 DbSets
        public DbSet<EmployerProfile> EmployerProfiles
            => Set<EmployerProfile>();

        public DbSet<Vacancy> Vacancies
            => Set<Vacancy>();

        public DbSet<Skill> Skills
            => Set<Skill>();

        public DbSet<VacancySkill> VacancySkills
            => Set<VacancySkill>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }

        // Member 4 DbSets
        public DbSet<Application> Applications
        => Set<Application>();
    }
}