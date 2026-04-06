using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Identifiers;

public sealed class QuestionId : ValueObject
{
    public Guid Value { get; }

    private QuestionId() { }

    private QuestionId(Guid value)
    {
        Value = value;
    }

    public static QuestionId CreateUnique()
        => new QuestionId(Guid.NewGuid());

    public static QuestionId From(Guid value)
        => new QuestionId(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
