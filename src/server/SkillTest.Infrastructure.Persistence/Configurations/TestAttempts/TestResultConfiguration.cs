using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.TestAttempts;

public sealed class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.ToTable("TestResults");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => ResultId.From(value))
            .ValueGeneratedNever();

        builder.Property(r => r.AttemptId)
            .HasConversion(
                id => id.Value,
                value => AttemptId.From(value))
            .IsRequired();

        builder.Property(r => r.QuestionId)
            .HasConversion(
                id => id.Value,
                value => QuestionId.From(value))
            .IsRequired();

        builder.Property(r => r.Percentage)
            .IsRequired();

        builder.Property(r => r.Score)
            .IsRequired();

        builder.Property(r => r.RewardPoints)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.HasMany(r => r.Answers)
            .WithOne()
            .HasForeignKey(a => a.ResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
