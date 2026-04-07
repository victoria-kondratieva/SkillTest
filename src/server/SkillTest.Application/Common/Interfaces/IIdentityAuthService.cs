using SkillTest.Application.Users.Enums;

namespace SkillTest.Application.Common.Interfaces;

public interface IIdentityAuthService
{
    Task<Guid> CreateIdentityUserAsync(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);

    Task<bool> ValidateCredentialsAsync(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);

    Task AssignRoleAsync(
        Guid userId, 
        UserRole roleName, 
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> GetRolesAsync(
        Guid userId, 
        CancellationToken cancellationToken = default);

    Task<Guid> GetUserIdByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default);
}
