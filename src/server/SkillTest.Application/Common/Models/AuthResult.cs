namespace SkillTest.Application.Common.Models;

public sealed class AuthResult
{
    public Guid UserId { get; init; }
    public required string Email { get; init; }
    public required string Token { get; init; }
}
