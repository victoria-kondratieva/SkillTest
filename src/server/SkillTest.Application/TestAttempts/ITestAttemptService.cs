using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Application.TestAttempts;

public interface ITestAttemptService
{
    Task<TestAttempt?> GetByIdAsync(
        AttemptId id, 
        CancellationToken ct = default);

    Task<TestAttempt> CreateAsync(
        UserId userId, 
        TestId testId, 
        CancellationToken ct = default);
    Task FinishAsync(AttemptId id, CancellationToken ct = default);
    Task CancelAsync(AttemptId id, CancellationToken ct = default);
}
