using Microsoft.Extensions.DependencyInjection;

using SkillTest.Application.Auth;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Application.TestAttempts;
using SkillTest.Application.Tests;
using SkillTest.Application.Users;

namespace SkillTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<ITestAttemptService, TestAttemptService>();

        return services;
    }
}
