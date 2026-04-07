using SkillTest.Application.Users.Enums;

namespace SkillTest.Application.Common.Interfaces;

public interface IIdentityUserService
{
    Task DeleteIdentityUserAsync(
        Guid id, 
        CancellationToken cancellationToken = default);
    Task<bool> AssignRoleAsync(
        Guid id, 
        UserRole role, 
        CancellationToken cancellationToken = default);
}
