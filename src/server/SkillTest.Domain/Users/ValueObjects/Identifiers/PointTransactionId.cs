using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Users.ValueObjects.Identifiers;

public sealed class PointTransactionId : ValueObject
{
    public Guid Value { get; }

    private PointTransactionId() { }

    private PointTransactionId(Guid value)
    {
        Value = value;
    }

    public static PointTransactionId CreateUnique() 
        => new PointTransactionId(Guid.NewGuid());

    public static PointTransactionId From(Guid value) 
        => new PointTransactionId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}