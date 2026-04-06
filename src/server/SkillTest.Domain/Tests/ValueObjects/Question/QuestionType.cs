using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Tests.ValueObjects.Question;

public sealed class QuestionType : ValueObject
{
    public string Value { get; }

    public static readonly QuestionType SingleChoice = new("SingleChoice");
    public static readonly QuestionType MultipleChoice = new("MultipleChoice");
    public static readonly QuestionType Text = new("Text");

    private QuestionType() { }

    private QuestionType(string value)
    {
        Value = value;
    }

    public static QuestionType From(string value)
    {
        return value switch
        {
            "SingleChoice" => SingleChoice,
            "MultipleChoice" => MultipleChoice,
            "Text" => Text,
            _ => throw new ArgumentException($"Invalid question type: {value}")
        };
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
