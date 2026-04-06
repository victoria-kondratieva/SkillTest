using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Shared;

public sealed class Tag : ValueObject
{
    public string Value { get; }

    private Tag() { }

    private Tag(string value)
    {
        Value = value;
    }

    public static Tag From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Tag cannot be empty");

        return new Tag(value.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;
}
