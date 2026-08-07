using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class JobSeekerSkillConfiguration : IEntityTypeConfiguration<JobSeekerSkill>
    {
        public void Configure(
           EntityTypeBuilder<JobSeekerSkill> builder)
        {
            builder.ToTable("JobSeekerSkills");

            // Composite primary key
            builder.HasKey(x => new
            {
                x.JobSeekerProfileId,
                x.SkillId
            });
 

            // Profile relationship
            builder.HasOne(x => x.JobSeekerProfile)
                .WithMany(x => x.JobSeekerSkills)
                .HasForeignKey(x => x.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Shared Skill relationship
            //builder.HasOne(x => x.Skill)
            //    .WithMany(x => x.JobSeekerSkills)
            //    .HasForeignKey(x => x.SkillId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Additional index for Skill-based searches
            builder.HasIndex(x => x.SkillId);
        }
    }
}
