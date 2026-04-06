using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Infrastructure.Identity.Services;

namespace SkillTest.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity(configuration);

        services.AddScoped<IIdentityAuthService, IdentityAuthService>();
        services.AddScoped<IIdentityUserService, IdentityUserService>();

        return services;
    }
}
