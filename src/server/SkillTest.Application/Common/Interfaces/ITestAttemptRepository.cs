using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

namespace SkillTest.Application.Common.Interfaces;

public interface ITestAttemptRepository
{
    Task<TestAttempt?> GetByIdAsync(AttemptId id, CancellationToken ct = default);
    void Add(TestAttempt attempt);
    void Update(TestAttempt attempt);
    void Remove(TestAttempt attempt);
}
