using Microsoft.AspNetCore.Identity;
using SkillTest.Infrastructure.Identity.Entities;
using SkillTest.Infrastructure.Identity.Roles;

namespace SkillTest.Infrastructure.Identity.Seed;

public static class DataSeed
{
    private static readonly Guid AdminId = Guid.Parse("3f4c2c8c-1c0e-4e4e-9b8e-0f7a4b6d2c11");

    public static async Task SeedAsync(
        UserManager<UserIdentity> userManager,
        RoleManager<RoleIdentity> roleManager)
    {
        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager);
    }

    public static async Task SeedAdminAsync(UserManager<UserIdentity> userManager)
    {
        const string email = "admin@example.com";
        const string password = "Admin123!";

        var admin = await userManager.FindByEmailAsync(email);
        if (admin == null)
        {
            admin = new UserIdentity
            {
                Id = AdminId,
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, RoleNames.Admin);
        }
    }

    private static async Task SeedRolesAsync(RoleManager<RoleIdentity> roleManager)
    {
        string[] roles =
        {
            RoleNames.Admin,
            RoleNames.User,
            RoleNames.Manager
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new RoleIdentity { Name = role });
            }
        }
    }
}