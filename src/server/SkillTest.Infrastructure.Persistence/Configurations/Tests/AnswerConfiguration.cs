using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.Tests;

public sealed class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answers");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AnswerId.From(value))
            .ValueGeneratedNever();

        builder.Property(a => a.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.IsCorrect)
            .IsRequired();

        builder.Property(a => a.OrderIndex)
            .IsRequired();
    }
}
