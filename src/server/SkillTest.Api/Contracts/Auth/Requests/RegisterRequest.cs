namespace SkillTest.Api.Contracts.Auth.Requests;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string Username,
    string FirstName,
    string LastName
);
