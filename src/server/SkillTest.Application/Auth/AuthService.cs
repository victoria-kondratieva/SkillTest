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
        CancellationToken cancellationToken = default)
    {
        var identityUserId = await _identityAuthService.CreateIdentityUserAsync(email, password, cancellationToken);

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

        await _userRepository.AddAsync(domainUser, cancellationToken);

        await _identityAuthService.AssignRoleAsync(identityUserId, UserRole.User, cancellationToken);

        var roles = await _identityAuthService.GetRolesAsync(identityUserId, cancellationToken);

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
        CancellationToken cancellationToken = default)
    {
        var isValid = await _identityAuthService.ValidateCredentialsAsync(email, password, cancellationToken);
        if (!isValid)
            throw new InvalidCredentialsException();

        var identityUserId = await _identityAuthService.GetUserIdByEmailAsync(email, cancellationToken);
        var roles = await _identityAuthService.GetRolesAsync(identityUserId, cancellationToken);

        var token = _jwtTokenService.GenerateToken(identityUserId, email, roles);

        return new AuthResult
        {
            UserId = identityUserId,
            Email = email,
            Token = token
        };
    }
}
