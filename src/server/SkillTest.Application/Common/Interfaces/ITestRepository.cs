using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Common.Interfaces;

public interface ITestRepository
{
    Task<Test?> GetByIdAsync(TestId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Test test, CancellationToken cancellationToken = default);
    Task UpdateAsync(Test test, CancellationToken cancellationToken = default);
    Task RemoveAsync(Test test, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(TestId id, CancellationToken cancellationToken = default);
}