using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(skill => skill.SkillId);

            builder.Property(skill => skill.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(skill => skill.Name)
                .IsUnique();
        }
    }
}