using SkillTest.Domain.Primitives;
using SkillTest.Domain.TestAttempts.ValueObjects;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Domain.TestAttempts.Entities;

public sealed class TestAttempt : Entity<AttemptId>, IAggregateRoot
{
    public UserId UserId { get; private set; }
    public TestId TestId { get; private set; }

    public DateTime StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public AttemptStatus Status { get; private set; }

    private readonly List<TestResult> _results = new();
    public IReadOnlyList<TestResult> Results => _results;

    private TestAttempt() { }

    public TestAttempt(
        AttemptId id,
        UserId userId,
        TestId testId)
        : base(id)
    {
        UserId = userId;
        TestId = testId;
        StartedAt = DateTime.UtcNow;
        Status = AttemptStatus.Started;
    }

    public void Finish()
    {
        Status = AttemptStatus.Finished;
        FinishedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = AttemptStatus.Cancelled;
        FinishedAt = DateTime.UtcNow;
    }

    public void AddResult(TestResult result)
    {
        _results.Add(result);
    }
}
