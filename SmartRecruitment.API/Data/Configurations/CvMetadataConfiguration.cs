using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class CvMetadataConfiguration
        : IEntityTypeConfiguration<CvMetadata>
    {
        public void Configure(EntityTypeBuilder<CvMetadata> builder)
        {
            builder.ToTable("CvMetadata");

            builder.HasKey(x => x.CvMetadataId);

            builder.Property(x => x.CvMetadataId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);
              

            builder.Property(x => x.FileSize)
                .IsRequired();

            builder.Property(x => x.UploadedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.JobSeekerProfileId)
                .IsUnique();

            builder.HasOne(x => x.JobSeekerProfile)
                .WithOne(x => x.CvMetadata)
                .HasForeignKey<CvMetadata>(x => x.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}