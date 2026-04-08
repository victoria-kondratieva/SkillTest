using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Tests;

public interface ITestService
{
    Task<Test?> GetByIdAsync(TestId id, CancellationToken ct = default);
    Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken ct = default);
    Task<Test> CreateAsync(Test test, CancellationToken ct = default);
    Task UpdateAsync(Test test, CancellationToken ct = default);
    Task DeleteAsync(TestId id, CancellationToken ct = default);
}
