using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

namespace SkillTest.Domain.TestAttempts.Entities;

public sealed class TestResult : Entity<ResultId>
{
    public AttemptId AttemptId { get; private set; }
    public QuestionId QuestionId { get; private set; }

    public double Percentage { get; private set; }
    public int Score { get; private set; }
    public int RewardPoints { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<TestResultAnswer> _answers = new();
    public IReadOnlyList<TestResultAnswer> Answers => _answers;

    private TestResult() { }

    public TestResult(
        ResultId id,
        AttemptId attemptId,
        QuestionId questionId,
        double percentage,
        int score,
        int rewardPoints)
        : base(id)
    {
        AttemptId = attemptId;
        QuestionId = questionId;
        Percentage = percentage;
        Score = score;
        RewardPoints = rewardPoints;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddAnswer(TestResultAnswer answer)
    {
        _answers.Add(answer);
    }
}
