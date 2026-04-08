using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Application.TestAttempts;

internal sealed class TestAttemptService : ITestAttemptService
{
    private readonly ITestAttemptRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TestAttemptService(
        ITestAttemptRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public Task<TestAttempt?> GetByIdAsync(AttemptId id, CancellationToken ct = default)
        => _repository.GetByIdAsync(id, ct);

    public async Task<TestAttempt> CreateAsync(
        UserId userId,
        TestId testId,
        CancellationToken ct = default)
    {
        var attempt = new TestAttempt(
            AttemptId.CreateUnique(),
            userId,
            testId);

        _repository.Add(attempt);
        await _unitOfWork.SaveChangesAsync(ct);

        return attempt;
    }

    public async Task FinishAsync(AttemptId id, CancellationToken ct = default)
    {
        var attempt = await _repository.GetByIdAsync(id, ct);
        if (attempt is null) return;

        attempt.Finish();
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task CancelAsync(AttemptId id, CancellationToken ct = default)
    {
        var attempt = await _repository.GetByIdAsync(id, ct);
        if (attempt is null) return;

        attempt.Cancel();
        await _unitOfWork.SaveChangesAsync(ct);
    }
}
