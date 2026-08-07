using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class VacancyConfiguration
        : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancies", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Vacancies_RequiredExperienceYears",
                    "[RequiredExperienceYears] >= 0");
            });

            // Primary Key
            builder.HasKey(vacancy => vacancy.VacancyId);

            // Properties
            builder.Property(vacancy => vacancy.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(vacancy => vacancy.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(vacancy => vacancy.Location)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(vacancy => vacancy.RequiredExperienceYears)
                .IsRequired();

            builder.Property(vacancy => vacancy.EducationRequirement)
                .IsRequired()
                .HasMaxLength(300);

            // Vacancy Status
            builder.Property(vacancy => vacancy.Status)
                .HasConversion<int>()
                .IsRequired();

            // EmployerProfile 1 -> many Vacancies
            builder.HasOne(vacancy => vacancy.EmployerProfile)
                .WithMany(employerProfile => employerProfile.Vacancies)
                .HasForeignKey(vacancy => vacancy.EmployerProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}