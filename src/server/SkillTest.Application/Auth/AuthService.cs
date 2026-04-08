using SkillTest.Application.Common.Exceptions;
using SkillTest.Application.Common.Interfaces;
using SkillTest.Application.Common.Models;
using SkillTest.Application.Users.Enums;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.User;

namespace SkillTest.Application.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IIdentityAuthService _identityAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IIdentityAuthService identityAuthService,
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService)
    {
        _identityAuthService = identityAuthService;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResult> RegisterAsync(
        string email,
        string password,
        string username,
        string firstName,
        string lastName,
        CancellationToken ct = default)
    {
        var identityUserId = await _identityAuthService.CreateIdentityUserAsync(email, password, ct);

        var userId = UserId.From(identityUserId);

        var domainUser = new User(
            userId,
            new Email(email),
            new UserProfile(
                new Username(username),
                new FullName(firstName, lastName)
            ),
            new UserSettings()
        );

        _userRepository.Add(domainUser);

        await _identityAuthService.AssignRoleAsync(identityUserId, UserRole.User, ct);

        var roles = await _identityAuthService.GetRolesAsync(identityUserId, ct);

        var token = _jwtTokenService.GenerateToken(identityUserId, email, roles);

        return new AuthResult
        {
            UserId = identityUserId,
            Email = email,
            Token = token
        };
    }

    public async Task<AuthResult> LoginAsync(
        string email,
        string password,
        CancellationToken ct = default)
    {
        var isValid = await _identityAuthService.ValidateCredentialsAsync(email, password, ct);
        if (!isValid)
            throw new InvalidCredentialsException();

        var identityUserId = await _identityAuthService.GetUserIdByEmailAsync(email, ct);
        var roles = await _identityAuthService.GetRolesAsync(identityUserId, ct);

        var token = _jwtTokenService.GenerateToken(identityUserId, email, roles);

        return new AuthResult
        {
            UserId = identityUserId,
            Email = email,
            Token = token
        };
    }
}
