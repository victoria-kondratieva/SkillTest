using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Identifiers;

public sealed class TestId : ValueObject
{
    public Guid Value { get; }

    private TestId() { }

    private TestId(Guid value)
    {
        Value = value;
    }

    public static TestId CreateUnique()
        => new TestId(Guid.NewGuid());

    public static TestId From(Guid value)
        => new TestId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
