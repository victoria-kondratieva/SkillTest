using Microsoft.EntityFrameworkCore;

using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(
        UserId id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Include(u => u.PointTransactions)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public Task UpdateAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Remove(user);
        return Task.CompletedTask;
    }
}
