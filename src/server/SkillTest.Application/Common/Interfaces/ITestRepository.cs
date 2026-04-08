using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Common.Interfaces;

public interface ITestRepository
{
    Task<Test?> GetByIdAsync(TestId id, CancellationToken ct = default);
    Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken ct = default);

    Task<bool> ExistsAsync(TestId id, CancellationToken ct = default);

    void Add(Test test);
    void Update(Test test);
    void Delete(Test test);
}