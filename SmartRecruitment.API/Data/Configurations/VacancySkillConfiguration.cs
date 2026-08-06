using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class VacancySkillConfiguration
        : IEntityTypeConfiguration<VacancySkill>
    {
        public void Configure(EntityTypeBuilder<VacancySkill> builder)
        {
            builder.HasKey(vacancySkill => new
            {
                vacancySkill.VacancyId,
                vacancySkill.SkillId
            });

            builder.HasOne(vacancySkill => vacancySkill.Vacancy)
                .WithMany(vacancy => vacancy.VacancySkills)
                .HasForeignKey(vacancySkill => vacancySkill.VacancyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vacancySkill => vacancySkill.Skill)
                .WithMany(skill => skill.VacancySkills)
                .HasForeignKey(vacancySkill => vacancySkill.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}