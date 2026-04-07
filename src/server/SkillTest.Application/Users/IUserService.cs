using SkillTest.Application.Users.Enums;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;

namespace SkillTest.Application.Users;

public interface IUserService
{
    Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(
        UserId id, 
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        User user, 
        CancellationToken cancellationToken = default);

    Task<(bool Success, User? User)> AddPointsAsync(
        UserId id,
        int amount,
        TransactionReason reason,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteUserAsync(
        Guid id, 
        CancellationToken cancellationToken = default);

    Task<bool> AssignRoleAsync(
        Guid id, 
        UserRole role, 
        CancellationToken cancellationToken = default);
}
