using Microsoft.AspNetCore.Identity;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Application.Users.Enums;
using SkillTest.Infrastructure.Identity.Entities;
using SkillTest.Infrastructure.Identity.Roles;

namespace SkillTest.Infrastructure.Identity.Services;

public sealed class IdentityUserService : IIdentityUserService
{
    private readonly UserManager<UserIdentity> _userManager;

    public IdentityUserService(UserManager<UserIdentity> userManager)
    {
        _userManager = userManager;
    }

    public async Task DeleteIdentityUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user != null)
            await _userManager.DeleteAsync(user);
    }

    public async Task<bool> AssignRoleAsync(Guid id, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            return false;

        var roleName = RoleNames.ToString(role);

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }
}
