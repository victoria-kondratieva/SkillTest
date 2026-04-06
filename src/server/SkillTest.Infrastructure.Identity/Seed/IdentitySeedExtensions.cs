using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SkillTest.Infrastructure.Identity.Entities;

namespace SkillTest.Infrastructure.Identity.Seed;

public static class IdentitySeedExtensions
{
    public static async Task SeedIdentityAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleIdentity>>();

        await DataSeed.SeedAsync(userManager, roleManager);
    }
}
