using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;

namespace SkillTest.Infrastructure.Persistence.Configurations.Users;

public sealed class PointTransactionConfiguration : IEntityTypeConfiguration<PointTransaction>
{
    public void Configure(EntityTypeBuilder<PointTransaction> builder)
    {
        builder.ToTable("PointTransactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => PointTransactionId.From(value))
            .ValueGeneratedNever();

        builder.Property(t => t.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.From(value))
            .IsRequired();

        builder.Property(t => t.Reason)
            .HasConversion(
                r => r.Value,
                value => TransactionReason.From(value))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();
    }
}
