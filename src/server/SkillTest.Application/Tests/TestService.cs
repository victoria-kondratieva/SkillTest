using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Tests;

internal sealed class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TestService(ITestRepository testRepository, IUnitOfWork unitOfWork)
    {
        _testRepository = testRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Test?> GetByIdAsync(
        TestId id,
        CancellationToken ct = default)
        => _testRepository.GetByIdAsync(id, ct);

    public Task<IReadOnlyList<Test>> GetAllAsync(
        CancellationToken ct = default)
        => _testRepository.GetAllAsync(ct);

    public async Task<Test> CreateAsync(
        Test test,
        CancellationToken ct = default)
    {
        _testRepository.Add(test);
        await _unitOfWork.SaveChangesAsync(ct);

        return test;
    }

    public async Task UpdateAsync(
        Test test,
        CancellationToken ct = default)
    {
        _testRepository.Update(test);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(
        TestId id,
        CancellationToken ct = default)
    {
        var test = await _testRepository.GetByIdAsync(id, ct);
        if (test is null)
            return;

        _testRepository.Delete(test);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}
