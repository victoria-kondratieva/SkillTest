using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.TestAttempts;

public sealed class TestAttemptConfiguration : IEntityTypeConfiguration<TestAttempt>
{
    public void Configure(EntityTypeBuilder<TestAttempt> builder)
    {
        builder.ToTable("TestAttempts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AttemptId.From(value))
            .ValueGeneratedNever();

        builder.Property(a => a.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value))
            .IsRequired();

        builder.Property(a => a.TestId)
            .HasConversion(
                id => id.Value,
                value => TestId.Create(value))
            .IsRequired();

        builder.Property(a => a.StartedAt)
            .IsRequired();

        builder.Property(a => a.FinishedAt);

        builder.Property(a => a.Status)
            .HasConversion(
                status => status.Value,
                value => AttemptStatus.From(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(a => a.Results)
            .WithOne()
            .HasForeignKey(r => r.AttemptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
