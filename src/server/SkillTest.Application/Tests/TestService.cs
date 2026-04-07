using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Application.Tests;

internal sealed class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TestService(
        ITestRepository testRepository,
        IUnitOfWork unitOfWork)
    {
        _testRepository = testRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Test?> GetByIdAsync(
        TestId id,
        CancellationToken cancellationToken = default)
        => _testRepository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Test>> GetAllAsync(
        CancellationToken cancellationToken = default)
        => _testRepository.GetAllAsync(cancellationToken);

    public async Task<Test> CreateAsync(
        Test test,
        CancellationToken cancellationToken = default)
    {
        await _testRepository.AddAsync(test, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return test;
    }

    public async Task UpdateAsync(
        Test test,
        CancellationToken cancellationToken = default)
    {
        await _testRepository.UpdateAsync(test, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        TestId id,
        CancellationToken cancellationToken = default)
    {
        var test = await _testRepository.GetByIdAsync(id, cancellationToken);

        if (test is null)
            return;

        await _testRepository.RemoveAsync(test, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
