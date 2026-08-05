using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasKey(vacancy => vacancy.VacancyId);

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

            builder.Property(vacancy => vacancy.IsClosed)
                .HasDefaultValue(false);

            builder.HasCheckConstraint(
                "CK_Vacancies_RequiredExperienceYears",
                "[RequiredExperienceYears] >= 0");
        }
    }
}