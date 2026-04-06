using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.User;

namespace SkillTest.Infrastructure.Persistence.Configurations.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value))
            .ValueGeneratedNever();

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.OwnsOne(u => u.Profile, profile =>
        {
            profile.Property(p => p.Position)
                .HasColumnName("Position")
                .HasMaxLength(200);

            profile.Property(p => p.AvatarUrl)
                .HasColumnName("AvatarUrl")
                .HasMaxLength(500);

            profile.OwnsOne(p => p.Username, username =>
            {
                username.Property(x => x.Value)
                    .HasColumnName("Username")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            profile.OwnsOne(p => p.FullName, fullName =>
            {
                fullName.Property(f => f.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired()
                    .HasMaxLength(100);

                fullName.Property(f => f.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(100);
            });
        });

        builder.OwnsOne(u => u.Settings, settings =>
        {
            settings.Property(s => s.EmailNotificationsEnabled)
                .HasColumnName("EmailNotificationsEnabled");

            settings.Property(s => s.TimeLimitSeconds)
                .HasColumnName("TimeLimitSeconds");

            settings.Property(s => s.AutoAdvanceEnabled)
                .HasColumnName("AutoAdvanceEnabled");

            settings.Property(s => s.Language)
                .HasColumnName("Language")
                .HasMaxLength(10);
        });

        builder.Property(u => u.TotalPoints)
            .HasConversion(
                p => p.Value,
                value => new Points(value))
            .IsRequired();

        builder.HasMany(u => u.PointTransactions)
            .WithOne()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
