using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class JobSeekerProfileConfiguration : IEntityTypeConfiguration<JobSeekerProfile>
    {
        public void Configure(
            EntityTypeBuilder<JobSeekerProfile> builder)
        {
            // Table name
            builder.ToTable("JobSeekerProfiles");

            // Primary key
            builder.HasKey(x => x.JobSeekerProfileId);

            builder.Property(x => x.JobSeekerProfileId)
                .ValueGeneratedOnAdd();

            // One User can have only one Job Seeker profile
            builder.HasIndex(x => x.UserId)
                .IsUnique();

            // Main profile fields
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            
            builder.Property(x => x.Address)
                .HasMaxLength(250);

            // Matching-related location fields
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Bio)
                .HasMaxLength(1000);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            // User 1 -> 0 or 1 JobSeekerProfile
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<JobSeekerProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // JobSeekerProfile 1 -> many JobSeekerSkills
            builder.HasMany(x => x.JobSeekerSkills)
                .WithOne(x => x.JobSeekerProfile)
                .HasForeignKey(x => x.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // JobSeekerProfile 1 -> many Education records
            builder.HasMany(x => x.Educations)
                .WithOne(x => x.JobSeekerProfile)
                .HasForeignKey(x => x.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // JobSeekerProfile 1 -> many Experience records
            builder.HasMany(x => x.Experiences)
                .WithOne(x => x.JobSeekerProfile)
                .HasForeignKey(x => x.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }

    }
}
