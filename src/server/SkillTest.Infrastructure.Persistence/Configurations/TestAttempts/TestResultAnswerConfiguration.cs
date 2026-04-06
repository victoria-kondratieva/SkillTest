using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.TestAttempts;

public sealed class TestResultAnswerConfiguration : IEntityTypeConfiguration<TestResultAnswer>
{
    public void Configure(EntityTypeBuilder<TestResultAnswer> builder)
    {
        builder.ToTable("TestResultAnswers");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => ResultAnswerId.From(value))
            .ValueGeneratedNever();

        builder.Property(a => a.ResultId)
            .HasConversion(
                id => id.Value,
                value => ResultId.From(value))
            .IsRequired();

        builder.Property(a => a.QuestionId)
            .HasConversion(
                id => id.Value,
                value => QuestionId.From(value))
            .IsRequired();

        builder.Property(a => a.AnswerId)
            .HasConversion(
                id => id.Value,
                value => AnswerId.From(value))
            .IsRequired();

        builder.Property(a => a.IsCorrect)
            .IsRequired();

        builder.Property(a => a.EarnedPoints)
            .IsRequired();
    }
}
