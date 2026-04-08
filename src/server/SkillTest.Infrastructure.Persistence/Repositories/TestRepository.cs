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
        CancellationToken ct = default)
    {
        return await _dbContext.Tests
            .Include(t => t.Category)
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<IReadOnlyList<Test>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Tests
            .Include(t => t.Category)
            .Include(t => t.Tags)
            .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(ct); 
    }

    public Task<bool> ExistsAsync(
    TestId id,
    CancellationToken ct = default)
    {
        return _dbContext.Tests
            .AnyAsync(t => t.Id == id, ct);
    }

    public void Add(Test test)
    {
        _dbContext.Tests.Add(test);
    }

    public void Update(Test test)
    {
        _dbContext.Tests.Update(test);
    }

    public void Delete(Test test)
    {
        _dbContext.Tests.Remove(test);
    }
}