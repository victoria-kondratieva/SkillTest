using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Configurations.Tests;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => CategoryId.From(value))
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Color)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Icon)
            .IsRequired()
            .HasMaxLength(100);
    }
}
