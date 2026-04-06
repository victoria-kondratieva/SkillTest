using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Test;

public sealed class DifficultyLevel : ValueObject
{
    public string Value { get; }

    public static readonly DifficultyLevel Beginner = new("Beginner");
    public static readonly DifficultyLevel Intermediate = new("Intermediate");
    public static readonly DifficultyLevel Advanced = new("Advanced");
    public static readonly DifficultyLevel Expert = new("Expert");

    private DifficultyLevel() { }

    private DifficultyLevel(string value)
    {
        Value = value;
    }

    public static DifficultyLevel From(string value)
    {
        return value switch
        {
            "Beginner" => Beginner,
            "Intermediate" => Intermediate,
            "Advanced" => Advanced,
            "Expert" => Expert,
            _ => throw new ArgumentException($"Invalid difficulty level: {value}")
        };
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
