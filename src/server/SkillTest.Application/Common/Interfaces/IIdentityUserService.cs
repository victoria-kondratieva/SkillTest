using SkillTest.Application.Users.Enums;

namespace SkillTest.Application.Common.Interfaces;

public interface IIdentityUserService
{
    Task DeleteIdentityUserAsync(Guid id);
    Task<bool> AssignRoleAsync(Guid id, UserRole role);
}
