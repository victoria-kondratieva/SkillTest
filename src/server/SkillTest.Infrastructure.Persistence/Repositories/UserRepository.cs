using Microsoft.EntityFrameworkCore;

using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        return await _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .Include(u => u.PointTransactions)
            .FirstOrDefaultAsync(u => u.Email.Value == email);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .Include(u => u.PointTransactions)
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
