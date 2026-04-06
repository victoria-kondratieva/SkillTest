namespace SkillTest.Api.Contracts.Auth.Responses;

public sealed record AuthCheckResponse(
    Guid UserId,
    string Email
);