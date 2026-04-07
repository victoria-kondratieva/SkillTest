using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Test;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.Tests;

public sealed class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Tests");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TestId.From(value))
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(t => t.Duration);

        builder.Property(t => t.MaxScore)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);

        builder.Property(t => t.Status)
            .HasConversion(
                status => status.Value,
                value => TestStatus.From(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.DifficultyLevel)
            .HasConversion(
                level => level.Value,
                value => DifficultyLevel.From(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.From(value))
            .IsRequired();

        builder.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId);

        builder.Property(t => t.CreatedBy)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value))
            .IsRequired();

        builder.OwnsMany(t => t.Tags, tags =>
        {
            tags.ToTable("TestTags");

            tags.WithOwner().HasForeignKey("TestId");

            tags.Property<Guid>("Id");
            tags.HasKey("Id");

            tags.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.HasMany(t => t.Questions)
            .WithOne()
            .HasForeignKey("TestId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
