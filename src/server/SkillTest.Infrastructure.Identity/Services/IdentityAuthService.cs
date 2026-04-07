using Microsoft.AspNetCore.Identity;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Application.Users.Enums;
using SkillTest.Infrastructure.Identity.Entities;
using SkillTest.Infrastructure.Identity.Roles;

namespace SkillTest.Infrastructure.Identity.Services;

public sealed class IdentityAuthService : IIdentityAuthService
{
    private readonly UserManager<UserIdentity> _userManager;
    private readonly SignInManager<UserIdentity> _signInManager;

    public IdentityAuthService(
        UserManager<UserIdentity> userManager,
        SignInManager<UserIdentity> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Guid> CreateIdentityUserAsync(
        string email, 
        string password, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = new UserIdentity
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

        return user.Id;
    }

    public async Task<bool> ValidateCredentialsAsync(
        string email, 
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return false;

        cancellationToken.ThrowIfCancellationRequested();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        return result.Succeeded;
    }

    public async Task AssignRoleAsync(
        Guid userId,
        UserRole role,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new Exception("User not found");

        cancellationToken.ThrowIfCancellationRequested();

        await _userManager.AddToRoleAsync(user, RoleNames.ToString(role));
    }

    public async Task<IReadOnlyList<string>> GetRolesAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new Exception("User not found");

        cancellationToken.ThrowIfCancellationRequested();

        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList().AsReadOnly();
    }

    public async Task<Guid> GetUserIdByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        cancellationToken.ThrowIfCancellationRequested();

        return user?.Id ?? throw new Exception("User not found");
    }
}