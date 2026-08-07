using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class EmployerProfileConfiguration
        : IEntityTypeConfiguration<EmployerProfile>
    {
        public void Configure(
            EntityTypeBuilder<EmployerProfile> builder)
        {
            builder.ToTable("EmployerProfiles");

            // Primary Key
            builder.HasKey(employer => employer.EmployerProfileId);

            // One User can have only one Employer Profile
            builder.HasIndex(employer => employer.UserId)
                .IsUnique();

            builder.Property(employer => employer.CompanyName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(employer => employer.CompanyDescription)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(employer => employer.Location)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(employer => employer.ContactNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(employer => employer.Website)
                .HasMaxLength(300);

            // User 1 -> 0 or 1 EmployerProfile
            builder.HasOne(employer => employer.User)
                .WithOne(user => user.EmployerProfile)
                .HasForeignKey<EmployerProfile>(
                    employer => employer.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployerProfile 1 -> many Vacancies
            builder.HasMany(employer => employer.Vacancies)
                .WithOne(vacancy => vacancy.EmployerProfile)
                .HasForeignKey(vacancy => vacancy.EmployerProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}