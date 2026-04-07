using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Tests;

public interface ITestService
{
    Task<Test?> GetByIdAsync(TestId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Test> CreateAsync(Test test, CancellationToken cancellationToken = default);
    Task UpdateAsync(Test test, CancellationToken cancellationToken = default);
    Task DeleteAsync(TestId id, CancellationToken cancellationToken = default);
}
