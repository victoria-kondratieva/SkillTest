using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

public sealed class ResultId : ValueObject
{
    public Guid Value { get; }

    private ResultId() { }

    private ResultId(Guid value)
    {
        Value = value;
    }

    public static ResultId CreateUnique()
        => new ResultId(Guid.NewGuid());

    public static ResultId From(Guid value)
        => new ResultId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
