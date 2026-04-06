using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Test;

public sealed class TestStatus : ValueObject
{
    public string Value { get; }

    public static readonly TestStatus Draft = new("Draft");
    public static readonly TestStatus Published = new("Published");
    public static readonly TestStatus Archived = new("Archived");
    public static readonly TestStatus InModeration = new("InModeration");

    private TestStatus() { }

    private TestStatus(string value)
    {
        Value = value;
    }

    public static TestStatus From(string value)
    {
        return value switch
        {
            "Draft" => Draft,
            "Published" => Published,
            "Archived" => Archived,
            "InModeration" => InModeration,
            _ => throw new ArgumentException($"Invalid test status: {value}")
        };
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
