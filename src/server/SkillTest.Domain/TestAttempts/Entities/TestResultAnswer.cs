using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

namespace SkillTest.Domain.TestAttempts.Entities;

public sealed class TestResultAnswer : Entity<ResultAnswerId>
{
    public ResultId ResultId { get; private set; }
    public QuestionId QuestionId { get; private set; }
    public AnswerId AnswerId { get; private set; }

    public bool IsCorrect { get; private set; }
    public int EarnedPoints { get; private set; }

    private TestResultAnswer() { }

    public TestResultAnswer(
        ResultAnswerId id,
        ResultId resultId,
        QuestionId questionId,
        AnswerId answerId,
        bool isCorrect,
        int earnedPoints)
        : base(id)
    {
        ResultId = resultId;
        QuestionId = questionId;
        AnswerId = answerId;
        IsCorrect = isCorrect;
        EarnedPoints = earnedPoints;
    }
}
