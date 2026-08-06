using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(
            EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.UserId);

            builder.Property(user => user.UserId)
                .ValueGeneratedOnAdd();

            builder.Property(user => user.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(user => user.Role)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(user => user.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(user => user.CreatedAt)
                .IsRequired();
        }
    }
}
