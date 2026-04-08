using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Question;
using SkillTest.Domain.Tests.ValueObjects.Shared;

namespace SkillTest.Domain.Tests.Entities;

public sealed class Question : Entity<QuestionId>
{
    public string Text { get; private set; }
    public QuestionType Type { get; private set; }
    public int Points { get; private set; }
    public int OrderIndex { get; private set; }

    private readonly List<Answer> _answers = new();
    public IReadOnlyList<Answer> Answers => _answers;

    private readonly List<Tag> _tags = new();
    public IReadOnlyList<Tag> Tags => _tags;

    private Question() { }

    public Question(QuestionId id, string text, QuestionType type, int points) : base(id)
    {
        Text = text;
        Type = type;
        Points = points;
        OrderIndex = 0;
    }

    public void Update(string text, QuestionType type, int points)
    {
        Text = text;
        Type = type;
        Points = points;
    }

    public void AddAnswer(Answer answer)
    {
        _answers.Add(answer);
    }

    public void RemoveAnswer(AnswerId answerId)
    {
        var answer = _answers.FirstOrDefault(a => a.Id == answerId);
        if (answer is null)
            return;

        _answers.Remove(answer);

        for (int i = 0; i < _answers.Count; i++)
            _answers[i].SetOrderIndex(i);
    }

    public void SetOrderIndex(int orderIndex)
    {
        OrderIndex = orderIndex;
    }

    public void AddTag(Tag tag)
    {
        if (!_tags.Contains(tag))
            _tags.Add(tag);
    }
}
