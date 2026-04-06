using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

public sealed class AttemptId : ValueObject
{
    public Guid Value { get; }

    private AttemptId() { }

    private AttemptId(Guid value)
    {
        Value = value;
    }

    public static AttemptId CreateUnique()
        => new AttemptId(Guid.NewGuid());

    public static AttemptId From(Guid value)
        => new AttemptId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
