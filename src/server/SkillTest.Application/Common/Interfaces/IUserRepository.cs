using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(string email);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
