using SkillTest.Domain.Primitives;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;

namespace SkillTest.Domain.Users.Entities;

public sealed class PointTransaction : Entity<PointTransactionId>
{
    public UserId UserId { get; private set; }
    public TransactionReason Reason { get; private set; }
    public int Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PointTransaction() { }

    private PointTransaction(PointTransactionId id, UserId userId, TransactionReason reason, int amount)
        : base(id)
    {
        UserId = userId;
        Reason = reason;
        Amount = amount;
        CreatedAt = DateTime.UtcNow;
    }

    public static PointTransaction Create(UserId userId, TransactionReason reason, int amount)
        => new PointTransaction(PointTransactionId.CreateUnique(), userId, reason, amount);
}
