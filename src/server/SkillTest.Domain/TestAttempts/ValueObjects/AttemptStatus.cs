using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.TestAttempts.ValueObjects;

public sealed class AttemptStatus : ValueObject
{
    public string Value { get; }

    public static readonly AttemptStatus Started = new("Started");
    public static readonly AttemptStatus Finished = new("Finished");
    public static readonly AttemptStatus Cancelled = new("Cancelled");

    private AttemptStatus() { }

    private AttemptStatus(string value)
    {
        Value = value;
    }

    public static AttemptStatus From(string value)
        => value switch
        {
            "Started" => Started,
            "Finished" => Finished,
            "Cancelled" => Cancelled,
            _ => throw new ArgumentException("Invalid attempt status")
        };

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
