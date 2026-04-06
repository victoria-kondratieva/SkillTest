using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Question;

namespace SkillTest.Infrastructure.Persistence.Configurations.Tests;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => QuestionId.From(value))
            .ValueGeneratedNever();

        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(q => q.Points)
            .IsRequired();

        builder.Property(q => q.OrderIndex)
            .IsRequired();

        builder.Property(q => q.Type)
            .HasConversion(
                type => type.Value,
                value => QuestionType.From(value))
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsMany(q => q.Tags, tags =>
        {
            tags.ToTable("QuestionTags");

            tags.WithOwner().HasForeignKey("QuestionId");

            tags.Property<Guid>("Id");
            tags.HasKey("Id");

            tags.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey("QuestionId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}