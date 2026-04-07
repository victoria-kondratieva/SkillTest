using Microsoft.Extensions.DependencyInjection;

using SkillTest.Application.Auth;
using SkillTest.Application.Users;
using SkillTest.Application.Tests;
using SkillTest.Application.Common.Interfaces;

namespace SkillTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITestService, TestService>();

        return services;
    }
}
