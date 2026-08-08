using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            builder.HasKey(cr => cr.ContactRequestId);

            builder.Property(cr => cr.Status)
                   .IsRequired();

            builder.Property(cr => cr.RequestedAt)
                   .IsRequired();

            builder.Property(cr => cr.RespondedAt)
                   .IsRequired(false);

            builder.HasOne(cr => cr.EmployerProfile)
                   .WithMany()
                   .HasForeignKey(cr => cr.EmployerProfileId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.JobSeekerProfile)
                   .WithMany()
                   .HasForeignKey(cr => cr.JobSeekerProfileId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
