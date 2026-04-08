using SkillTest.Application.Users.Enums;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;

namespace SkillTest.Application.Users;

public interface IUserService
{
    Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken ct = default);

    Task<User?> GetByIdAsync(
        UserId id, 
        CancellationToken ct = default);

    Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken ct = default);

    Task CreateAsync(
        User user, 
        CancellationToken ct = default);

    Task<(bool Success, User? User)> AddPointsAsync(
        UserId id,
        int amount,
        TransactionReason reason,
        CancellationToken ct = default);

    Task<bool> DeleteUserAsync(
        Guid id, 
        CancellationToken ct = default);

    Task<bool> AssignRoleAsync(
        Guid id, 
        UserRole role, 
        CancellationToken ct = default);
}
