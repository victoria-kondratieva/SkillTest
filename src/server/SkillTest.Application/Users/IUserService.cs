using SkillTest.Application.Users.Enums;

namespace SkillTest.Application.Users;

public interface IUserService
{
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> AssignRoleAsync(Guid id, UserRole role);
}
