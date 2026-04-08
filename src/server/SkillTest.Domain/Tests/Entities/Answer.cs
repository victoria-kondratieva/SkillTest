using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Domain.Tests.Entities;

public sealed class Answer : Entity<AnswerId>
{
    public string Text { get; private set; }
    public bool IsCorrect { get; private set; }
    public int OrderIndex { get; private set; }

    private Answer() { }

    public Answer(AnswerId id, string text, bool isCorrect) : base(id)
    {
        Text = text;
        IsCorrect = isCorrect;
        OrderIndex = 0;
    }

    public void Update(string text, bool isCorrect)
    {
        Text = text;
        IsCorrect = isCorrect;
    }

    public void SetOrderIndex(int orderIndex)
    {
        OrderIndex = orderIndex;
    }
}
