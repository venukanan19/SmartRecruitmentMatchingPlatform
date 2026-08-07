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
            builder.HasKey(employer => employer.EmployerProfileId);

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

            builder.HasMany(employer => employer.Vacancies)
                .WithOne(vacancy => vacancy.EmployerProfile)
                .HasForeignKey(vacancy => vacancy.EmployerProfileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}