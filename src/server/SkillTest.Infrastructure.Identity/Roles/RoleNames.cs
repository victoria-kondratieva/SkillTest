using SkillTest.Application.Users.Enums;

namespace SkillTest.Infrastructure.Identity.Roles;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Manager = "Manager";

    public static string ToString(UserRole role) =>
        role switch
        {
            UserRole.Admin => Admin,
            UserRole.User => User,
            UserRole.Manager => Manager,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
}
