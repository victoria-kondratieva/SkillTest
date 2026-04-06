using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

public sealed class ResultAnswerId : ValueObject
{
    public Guid Value { get; }

    private ResultAnswerId() { }

    private ResultAnswerId(Guid value)
    {
        Value = value;
    }

    public static ResultAnswerId CreateUnique()
        => new ResultAnswerId(Guid.NewGuid());

    public static ResultAnswerId From(Guid value)
        => new ResultAnswerId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
