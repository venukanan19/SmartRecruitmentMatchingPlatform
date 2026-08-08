using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            // Table mapping
            builder.ToTable("Applications");

            // Primary Key
            builder.HasKey(a => a.ApplicationId);

            // Property Constraints
            builder.Property(a => a.CoverLetter)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(a => a.AppliedDate)
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .IsRequired(false);

            // Unique Constraint: Prevents duplicate applications for the same job seeker and vacancy
            builder.HasIndex(a => new { a.JobSeekerProfileId, a.VacancyId })
                .IsUnique()
                .HasDatabaseName("IX_Applications_JobSeekerProfileId_VacancyId_Unique");

            // Foreign Key Relationships
            
            
            builder.HasOne(a => a.JobSeekerProfile)
                .WithMany()
                .HasForeignKey(a => a.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Vacancy)
                .WithMany()
                .HasForeignKey(a => a.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}