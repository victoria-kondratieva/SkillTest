namespace SkillTest.Api.Contracts.Auth.Requests;

public sealed record LoginRequest(
    string Email,
    string Password
);
