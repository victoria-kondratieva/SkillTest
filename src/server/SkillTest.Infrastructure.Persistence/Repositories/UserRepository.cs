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
        CancellationToken ct = default)
    {
        return _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public Task<User?> GetByEmailAsync(
        string email,
        CancellationToken ct = default)
    {
        return _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Email.Value == email, ct);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await _dbContext.Users
            .Include(u => u.PointTransactions)
            .ToListAsync(ct);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }
}
