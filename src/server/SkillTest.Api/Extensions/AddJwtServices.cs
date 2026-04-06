using Microsoft.Extensions.Options;
using SkillTest.Api.Auth;
using SkillTest.Application.Common.Interfaces;

namespace SkillTest.Api.Extensions;

public static class JwtServicesExtensions
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JWT"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}