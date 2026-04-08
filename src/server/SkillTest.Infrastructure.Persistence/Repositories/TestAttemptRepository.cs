using Microsoft.EntityFrameworkCore;

using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Repositories;

internal sealed class TestAttemptRepository : ITestAttemptRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestAttemptRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<TestAttempt?> GetByIdAsync(AttemptId id, CancellationToken ct = default)
    {
        return _dbContext.TestAttempts
            .Include(a => a.Results)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public void Add(TestAttempt attempt)
    {
        _dbContext.TestAttempts.Add(attempt);
    }

    public void Update(TestAttempt attempt)
    {
        _dbContext.TestAttempts.Update(attempt);
    }

    public void Remove(TestAttempt attempt)
    {
        _dbContext.TestAttempts.Remove(attempt);
    }
}
