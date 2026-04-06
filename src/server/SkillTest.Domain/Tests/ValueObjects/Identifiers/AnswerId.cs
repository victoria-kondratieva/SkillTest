using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Identifiers;

public sealed class AnswerId : ValueObject
{
    public Guid Value { get; }

    private AnswerId() { }

    private AnswerId(Guid value)
    {
        Value = value;
    }

    public static AnswerId CreateUnique()
        => new AnswerId(Guid.NewGuid());

    public static AnswerId From(Guid value)
        => new AnswerId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
