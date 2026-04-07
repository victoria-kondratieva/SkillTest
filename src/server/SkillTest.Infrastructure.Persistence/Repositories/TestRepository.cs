using Microsoft.EntityFrameworkCore;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Repositories;

internal sealed class TestRepository : ITestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Test?> GetByIdAsync(
        TestId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tests
            .Include(t => t.Category)
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tests
            .Include(t => t.Category)
            .Include(t => t.Tags)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken); 
    }

    public async Task AddAsync(
        Test test,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Tests.AddAsync(test, cancellationToken);
    }

    public Task UpdateAsync(
        Test test, 
        CancellationToken cancellationToken = default)
    {
        _dbContext.Tests.Update(test);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(
        Test test,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Tests.Remove(test);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(
        TestId id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Tests
            .AnyAsync(t => t.Id == id, cancellationToken);
    }
}