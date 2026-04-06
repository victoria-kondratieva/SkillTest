using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Users.ValueObjects.PointTransaction;

public sealed class TransactionReason : ValueObject
{
    public string Value { get; }

    private TransactionReason(string value)
    {
        Value = value;
    }

    public static readonly TransactionReason TestCompleted = new("TestCompleted");
    public static readonly TransactionReason Bonus = new("Bonus");
    public static readonly TransactionReason ManualAdjustment = new("ManualAdjustment");

    public static TransactionReason From(string value)
        => new TransactionReason(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
