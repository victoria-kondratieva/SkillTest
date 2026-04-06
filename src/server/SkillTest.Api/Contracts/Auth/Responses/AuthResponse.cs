namespace SkillTest.Api.Contracts.Auth.Responses;

public sealed record AuthResponse(
    Guid UserId,
    string Email,
    string Token
);
