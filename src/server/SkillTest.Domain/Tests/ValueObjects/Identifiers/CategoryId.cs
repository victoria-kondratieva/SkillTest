using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Identifiers;

public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }

    private CategoryId() { }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateUnique()
        => new CategoryId(Guid.NewGuid());

    public static CategoryId From(Guid value)
        => new CategoryId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
